using Lexicom.Authority;
using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Database.Entities;
using Lexicom.Jwt;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Security.Claims;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Services;

public interface IJwtService
{
    /// <exception cref="UserDoesNotExistException"/>
    Task<BearerToken> GenerateAccessTokenAsync(Guid userId);
    /// <exception cref="UserDoesNotExistException"/>
    Task<BearerToken> GenerateRefreshTokenAsync(Guid userId, Guid accessTokenJti);
}
public class JwtService : IJwtService
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IAccessTokenProvider _accessTokenProvider;
    private readonly IRefreshTokenProvider _refreshTokenProvider;
    private readonly IRefreshTokenEntryService _refreshTokenEntryService;

    public JwtService(
        IUserService userService,
        IRoleService roleService,
        IAccessTokenProvider accessTokenProvider,
        IRefreshTokenProvider refreshTokenProvider,
        IRefreshTokenEntryService refreshTokenEntryService)
    {
        _userService = userService;
        _roleService = roleService;
        _accessTokenProvider = accessTokenProvider;
        _refreshTokenProvider = refreshTokenProvider;
        _refreshTokenEntryService = refreshTokenEntryService;
    }

    public async Task<BearerToken> GenerateAccessTokenAsync(Guid userId)
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

    public async Task<BearerToken> GenerateRefreshTokenAsync(Guid userId, Guid accessTokenJti)
    {
        User user = await _userService.GetUserByIdAsync(userId);

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString().ToLowerInvariant()),
        };

        BearerToken bearerToken = await _refreshTokenProvider.CreateRefreshTokenAsync(claims);

        await _refreshTokenEntryService.CreateRefreshTokenEntryAsync(userId, accessTokenJti, bearerToken);

        return bearerToken;
    }
}
