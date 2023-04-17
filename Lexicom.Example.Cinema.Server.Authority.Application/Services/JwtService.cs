using Lexicom.Authority;
using Lexicom.DependencyInjection.Primitives;
using Lexicom.Example.Cinema.Server.Authority.Application.Database;
using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Jwt;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Services;
public interface IJwtService
{
    /// <exception cref="UserDoesNotExistException"/>
    Task<BearerToken> CreateAccessTokenAsync(Guid userId);
    /// <exception cref="UserDoesNotExistException"/>
    Task<BearerToken> CreateRefreshTokenAsync(Guid userId, Guid accessTokenJti);
}
public class JwtService : IJwtService
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IAccessTokenProvider _accessTokenProvider;
    private readonly IRefreshTokenProvider _refreshTokenProvider;
    private readonly IDbContextFactory<AuthorityDbContext> _dbContextFactory;
    private readonly ITimeProvider _timeProvider;

    public JwtService(
        IUserService userService,
        IRoleService roleService,
        IAccessTokenProvider accessTokenProvider,
        IRefreshTokenProvider refreshTokenProvider,
        IDbContextFactory<AuthorityDbContext> dbContextFactory,
        ITimeProvider timeProvider)
    {
        _userService = userService;
        _roleService = roleService;
        _accessTokenProvider = accessTokenProvider;
        _refreshTokenProvider = refreshTokenProvider;
        _dbContextFactory = dbContextFactory;
        _timeProvider = timeProvider;
    }

    public async Task<BearerToken> CreateAccessTokenAsync(Guid userId)
    {
        User user = await _userService.GetUserByIdAsync(userId);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString().ToLowerInvariant()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email.ToLowerInvariant()),
        };

        //add all roles and permissions the user has to the claims
        IReadOnlyList<Role> roles;
        try
        {
            roles = await _userService.GetUserRolesAsync(user.Id);
        }
        catch (UserDoesNotExistException e)
        {
            throw e.ToUnreachableException();
        }

        foreach (Role role in roles)
        {
            string roleClaimValue = role.Name.ToLowerInvariant();

            bool roleClaimAlreadyAdded = claims.Any(c => c.Type == ClaimTypes.Role && c.Value == roleClaimValue);
            if (!roleClaimAlreadyAdded)
            {
                claims.Add(new Claim(ClaimTypes.Role, roleClaimValue));
            }

            IReadOnlyList<string> permissions;
            try
            {
                permissions = await _roleService.GetRolePermissionsAsync(role.Id);
            }
            catch (RoleDoesNotExistException e)
            {
                throw e.ToUnreachableException();
            }

            foreach (string permission in permissions)
            {
                string permissionClaimValue = permission.ToLowerInvariant();

                bool permissionClaimAlreadyAdded = claims.Any(c => c.Type == LexicomJwtClaimTypes.Permission && c.Value == permissionClaimValue);
                if (!permissionClaimAlreadyAdded)
                {
                    claims.Add(new Claim(LexicomJwtClaimTypes.Permission, permissionClaimValue));
                }
            }
        }

        return await _accessTokenProvider.CreateAccessTokenAsync(claims);
    }

    public async Task<BearerToken> CreateRefreshTokenAsync(Guid userId, Guid accessTokenJti)
    {
        User user = await _userService.GetUserByIdAsync(userId);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString().ToLowerInvariant()),
        };

        BearerToken bearerToken = await _refreshTokenProvider.CreateRefreshTokenAsync(claims);

        using var db = await _dbContextFactory.CreateDbContextAsync();

        RefreshToken? userRefreshToken = await db.RefreshTokens.FirstOrDefaultAsync(urt => urt.UserId == user.Id);

        if (userRefreshToken is not null)
        {
            db.RefreshTokens.Remove(userRefreshToken);
        }

        userRefreshToken = new RefreshToken
        {
            Id = bearerToken.Jti,
            UserId = user.Id,
            AccessTokenJti = accessTokenJti,
            CreatedDateTimeOffsetUtc = _timeProvider.UtcNow,
            ExpiresDateTimeOffsetUtc = bearerToken.Expires,
        };

        await db.RefreshTokens.AddAsync(userRefreshToken);

        await db.SaveChangesAsync();

        return bearerToken;
    }
}
