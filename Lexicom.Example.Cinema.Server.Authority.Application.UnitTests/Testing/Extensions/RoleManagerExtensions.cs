using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Jwt;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Lexicom.Example.Cinema.Server.Authority.Application.UnitTests.Testing.Extensions;
public static class RoleManagerExtensions
{
    public static async Task<Role> CreateAsync(this RoleManager<Role> roleManager, string? name = null)
    {
        name ??= $"{Guid.NewGuid().ToString().ToLowerInvariant()}role";

        var role = new Role
        {
            Id = Guid.NewGuid(),
            Name = name,
        };

        await roleManager.CreateAsync(role);

        return role;
    }

    public static async Task<List<string>> GetPermissionsAsync(this RoleManager<Role> roleManager, Role role)
    {
        IList<Claim> claims = await roleManager.GetClaimsAsync(role);

        return claims
            .Where(c => c.Type == LexicomJwtClaimTypes.Permission)
            .Select(c => c.Value)
            .ToList();
    }

    public static async Task<string> AddPermissionAsync(this RoleManager<Role> roleManager, Role role, string? permission = null)
    {
        permission ??= $"{Guid.NewGuid().ToString().ToLowerInvariant()}permission";

        var permissionClaim = new Claim(LexicomJwtClaimTypes.Permission, permission);

        await roleManager.AddClaimAsync(role, permissionClaim);

        return permission;
    }
}
