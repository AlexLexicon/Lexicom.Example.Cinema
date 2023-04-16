using Lexicom.Authority;
using Lexicom.DependencyInjection.Primitives;
using Lexicom.EntityFramework.Amenities.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Database;
using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Jwt.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Services;
public interface ISignInService
{
    /// <exception cref="UserDoesNotExistException"/>
    /// <exception cref="UserLockedOutException"/>
    /// <exception cref="UserNotVerifiedException"/>
    /// <exception cref="PasswordIncorrectException"/>
    Task<SignIn> SignInUserAsync(string email, string password);
    /// <exception cref="AccessBearerTokenNotValidException"/>
    /// <exception cref="RefreshBearerTokenNotValidException"/>
    /// <exception cref="RefreshTokenAccessTokenSubjectMismatchException"/>
    /// <exception cref="RefreshTokenDoesNotExistException"/>
    /// <exception cref="RefreshTokenAccessTokenJtiMismatchException"/>
    /// <exception cref="RefreshTokenUserMismatchException"/>
    /// <exception cref="RefreshTokenExpiredException"/>
    Task<SignIn> RefreshUserAsync(string accessBearerToken, string refreshBearerToken);
}
public class SignInService : ISignInService
{
    private readonly IUserService _userService;
    private readonly SignInManager<User> _signInManager;
    private readonly IJwtService _jwtService;
    private readonly IAccessTokenProvider _accessTokenService;
    private readonly IRefreshTokenProvider _refreshTokenProvider;
    private readonly UserManager<User> _userManager;
    private readonly ITimeProvider _timeProvider;
    private readonly IDbContextFactory<AuthorityDbContext> _dbContextFactory;

    public SignInService(
        IUserService userService,
        SignInManager<User> signInManager,
        IJwtService jwtService,
        IAccessTokenProvider accessTokenProvider,
        IRefreshTokenProvider refreshTokenProvider,
        UserManager<User> userManager,
        ITimeProvider timeProvider,
        IDbContextFactory<AuthorityDbContext> dbContextFactory)
    {
        _userService = userService;
        _signInManager = signInManager;
        _jwtService = jwtService;
        _accessTokenService = accessTokenProvider;
        _userManager = userManager;
        _timeProvider = timeProvider;
        _dbContextFactory = dbContextFactory;
        _refreshTokenProvider = refreshTokenProvider;
    }

    public async Task<SignIn> SignInUserAsync(string email, string password)
    {
        User user = await _userService.GetUserByEmailAsync(email);

        NonNullableTableColumnException.ThrowIfNull(user.Email);

        SignInResult signInIdentityResult = await _signInManager.CheckPasswordSignInAsync(user, password, lockoutOnFailure: true);

        if (!signInIdentityResult.Succeeded)
        {
            if (signInIdentityResult.IsLockedOut)
            {
                throw new UserLockedOutException(user.Id);
            }

            //a user can not sign in if they need to confirm their email, phone or [account] (as in identity account).
            if (signInIdentityResult.IsNotAllowed)
            {
                throw new UserNotVerifiedException(user.Id);
            }

            if (signInIdentityResult.RequiresTwoFactor)
            {
                //we currently do not support two factor authentication
                throw new NotImplementedException();
            }

            throw new PasswordIncorrectException();
        }

        return await SignInUserAsync(user);
    }

    public async Task<SignIn> RefreshUserAsync(string accessBearerToken, string refreshBearerToken)
    {
        var isAccessTokenValidTask = _accessTokenService.IsAccessTokenValidAsync(accessBearerToken, validateLifetime: false);
        var isRefreshTokenValidTask = _refreshTokenProvider.IsRefreshTokenValidAsync(refreshBearerToken);

        bool isAccessTokenValid = await isAccessTokenValidTask;

        if (!isAccessTokenValid)
        {
            throw new AccessBearerTokenNotValidException(accessBearerToken);
        }

        bool isRefreshTokenValid = await isRefreshTokenValidTask;

        if (!isRefreshTokenValid)
        {
            throw new RefreshBearerTokenNotValidException(refreshBearerToken);
        }

        JwtSecurityToken accessSecurityToken = accessBearerToken.ToJwtSecurityToken();
        JwtSecurityToken refreshSecurityToken = refreshBearerToken.ToJwtSecurityToken();

        Guid accessTokenUserId = accessSecurityToken.GetSubjectId();
        Guid refreshTokenUserId = refreshSecurityToken.GetSubjectId();

        Guid accessTokenJti = accessSecurityToken.GetJti();
        Guid refreshTokenJti = refreshSecurityToken.GetJti();

        using var db = await _dbContextFactory.CreateDbContextAsync();

        RefreshToken? refreshToken = await db.RefreshTokens.FirstOrDefaultAsync(urt => urt.Id == refreshTokenJti);

        if (refreshToken is null)
        {
            throw new RefreshTokenDoesNotExistException(refreshTokenJti);
        }

        if (accessTokenUserId != refreshTokenUserId)
        {
            db.RefreshTokens.Remove(refreshToken);
            await db.SaveChangesAsync();

            throw new RefreshTokenAccessTokenSubjectMismatchException(accessTokenUserId, refreshTokenUserId);
        }

        if (refreshToken.AccessTokenJti != accessTokenJti)
        {
            db.RefreshTokens.Remove(refreshToken);
            await db.SaveChangesAsync();

            throw new RefreshTokenAccessTokenJtiMismatchException(refreshToken.Id, refreshToken.AccessTokenJti, accessTokenJti);
        }

        if (refreshToken.UserId != accessTokenUserId)
        {
            db.RefreshTokens.Remove(refreshToken);
            await db.SaveChangesAsync();

            throw new RefreshTokenUserMismatchException(refreshToken.Id, refreshToken.UserId, accessTokenUserId);
        }

        //the tokens experation should automatically be checked for experation
        //above then we call 'IsBearerTokenValidAsync(refreshToken)' but
        //just incase we check here as well since someone might
        //want to modify the token expires date in the database to 
        //invalidate a token early
        if (refreshToken.ExpiresDateTimeOffsetUtc < _timeProvider.UtcNow)
        {
            db.RefreshTokens.Remove(refreshToken);
            await db.SaveChangesAsync();

            throw new RefreshTokenExpiredException(refreshToken.Id);
        }

        User user = await _userService.GetUserByIdAsync(refreshToken.UserId);

        return await SignInUserAsync(user);
    }

    private async Task<SignIn> SignInUserAsync(User user)
    {
        BearerToken accessBearerToken;
        BearerToken refreshBearerToken;

        var createAccessTokenTask = _jwtService.CreateAccessTokenAsync(user.Id);
        var createRefreshTokenTask = _jwtService.CreateRefreshTokenAsync(user.Id);

        try
        {
            accessBearerToken = await createAccessTokenTask;
            refreshBearerToken = await createRefreshTokenTask;
        }
        catch (UserDoesNotExistException e)
        {
            throw e.ToUnreachableException();
        }

        user.LastSignInDateTimeOffsetUtc = _timeProvider.UtcNow;

        try
        {
            await _userManager.UpdateAsync(user);
        }
        catch (UserDoesNotExistException e)
        {
            //if the refresh token 'UserId' matches the access token
            //then it should be safe to assume the user exists by this point
            throw e.ToUnreachableException();
        }

        return new SignIn
        {
            AccessToken = accessBearerToken.Value,
            RefreshToken = refreshBearerToken.Value,
        };
    }
}
