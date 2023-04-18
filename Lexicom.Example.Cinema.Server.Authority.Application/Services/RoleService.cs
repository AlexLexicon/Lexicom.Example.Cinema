using Lexicom.DependencyInjection.Primitives;
using Lexicom.EntityFramework.Identity.Exceptions;
using Lexicom.EntityFramework.Identity.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Jwt;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Services;
public interface IRoleService
{
    /// <exception cref="RoleDoesNotExistException"/>
    Task<Role> GetRoleByIdAsync(Guid roleId);
    /// <exception cref="RoleDoesNotExistException"/>
    Task<Role> GetRoleByNameAsync(string name);
    /// <exception cref="RoleDoesNotExistException"/>
    Task<ComprehensiveRole> GetComprehensiveRoleAsync(Guid roleId);
    /// <exception cref="RoleDoesNotExistException"/>
    Task<IReadOnlyList<string>> GetRolePermissionsAsync(Guid roleId);
    /// <exception cref="RoleAlreadyExistsException"/>
    /// <exception cref="RoleNameNotValidException"/>
    Task<Role> CreateRoleAsync(string name);
    /// <exception cref="RoleDoesNotExistException"/>
    /// <exception cref="RoleNameAlreadyInUseException"/>
    Task UpdateRoleAsync(Guid roleId, string? newName);
    /// <exception cref="PermissionDoesNotExistException"/>
    /// <exception cref="RoleDoesNotExistException"/>
    /// <exception cref="RoleAlreadyHasPermissionException"/>
    Task AddPermissionToRoleAsync(Guid roleId, string permission);
    /// <exception cref="RoleDoesNotExistException"/>
    /// <exception cref="RoleDoesNotHavePermissionException"/>
    Task RemovePermissionFromRoleAsync(Guid roleId, string permission);
}
public class RoleService : IRoleService
{
    private readonly RoleManager<Role> _roleManager;
    private readonly IPermissionService _permissionService;
    private readonly IGuidProvider _guidProvider;
    private readonly ITimeProvider _timeProvider;

    public RoleService(
        RoleManager<Role> roleManager,
        IPermissionService permissionService,
        IGuidProvider guidProvider,
        ITimeProvider timeProvider)
    {
        _roleManager = roleManager;
        _permissionService = permissionService;
        _guidProvider = guidProvider;
        _timeProvider = timeProvider;
    }

    public async Task<Role> GetRoleByIdAsync(Guid roleId)
    {
        Role? role = await _roleManager.FindByIdAsync(roleId.ToString());

        if (role is null)
        {
            throw new RoleDoesNotExistException(roleId);
        }

        return role;
    }

    public async Task<Role> GetRoleByNameAsync(string name)
    {
        Role? role = await _roleManager.FindByNameAsync(name);

        if (role is null)
        {
            throw new RoleDoesNotExistException(name);
        }

        return role;
    }

    public async Task<ComprehensiveRole> GetComprehensiveRoleAsync(Guid roleId)
    {
        Role role = await GetRoleByIdAsync(roleId);

        IReadOnlyList<string> permissions;
        try
        {
            permissions = await GetRolePermissionsAsync(role.Id);
        }
        catch (RoleDoesNotExistException e)
        {
            throw e.ToUnreachableException();
        }

        return new ComprehensiveRole
        {
            Id = role.Id,
            Name = role.Name,
            Permissions = permissions,
        };
    }

    public async Task<IReadOnlyList<string>> GetRolePermissionsAsync(Guid roleId)
    {
        Role role = await GetRoleByIdAsync(roleId);

        IList<Claim> claims = await _roleManager.GetClaimsAsync(role);

        return claims
            .Where(c => c.Type == LexicomJwtClaimTypes.Permission)
            .Select(c => c.Value)
            .ToList();
    }

    public async Task<Role> CreateRoleAsync(string name)
    {
        bool roleExists = await _roleManager.RoleExistsAsync(name);

        if (roleExists)
        {
            throw new RoleAlreadyExistsException(name);
        }

        var role = new Role
        {
            Id = _guidProvider.NewGuid(),
            Name = name,
            CreatedDateTimeOffsetUtc = _timeProvider.UtcNow,
        };

        IdentityResult createRoleIdentityResult = await _roleManager.CreateAsync(role);

        if (!createRoleIdentityResult.Succeeded)
        {
            if (createRoleIdentityResult.HasInvalidRoleNameError())
            {
                throw new RoleNameNotValidException(name);
            }

            throw new IdentityResultException(createRoleIdentityResult);
        }

        return role;
    }

    public async Task UpdateRoleAsync(Guid roleId, string? newName)
    {
        Role role = await GetRoleByIdAsync(roleId);

        if (newName is not null)
        {
            Role? roleWithNewName = await _roleManager.FindByNameAsync(newName);

            if (role != roleWithNewName && roleWithNewName is not null)
            {
                throw new RoleNameAlreadyInUseException(newName);
            }

            IdentityResult setRoleNameIdentityResult = await _roleManager.SetRoleNameAsync(role, newName);

            if (!setRoleNameIdentityResult.Succeeded)
            {
                throw new IdentityResultException(setRoleNameIdentityResult);
            }
        }
    }

    public async Task AddPermissionToRoleAsync(Guid roleId, string permission)
    {
        bool permissionExists = await _permissionService.PermissionExistsAsync(permission);

        if (!permissionExists)
        {
            throw new PermissionDoesNotExistException(permission);
        }

        Role role = await GetRoleByIdAsync(roleId);

        IList<Claim> roleClaims = await _roleManager.GetClaimsAsync(role);

        bool roleAlreadyHasPermission = roleClaims.Any(c => c.Type == LexicomJwtClaimTypes.Permission && c.Value == permission);

        if (roleAlreadyHasPermission)
        {
            throw new RoleAlreadyHasPermissionException(roleId, permission);
        }

        var permissionClaim = new Claim(LexicomJwtClaimTypes.Permission, permission);

        IdentityResult addClaimIdentityResult = await _roleManager.AddClaimAsync(role, permissionClaim);

        if (!addClaimIdentityResult.Succeeded)
        {
            throw new IdentityResultException(addClaimIdentityResult);
        }
    }

    public async Task RemovePermissionFromRoleAsync(Guid roleId, string permission)
    {
        //we dont check for if the permission exists from the IPermissionService
        //because maybe we remove a permission in the future and want to remove
        //it from all the roles via this method

        Role role = await GetRoleByIdAsync(roleId);

        IList<Claim> roleClaims = await _roleManager.GetClaimsAsync(role);

        Claim? permissionClaim = roleClaims.FirstOrDefault(c => c.Type == LexicomJwtClaimTypes.Permission && c.Value == permission);

        if (permissionClaim is null)
        {
            throw new RoleDoesNotHavePermissionException(roleId, permission);
        }

        IdentityResult addClaimIdentityResult = await _roleManager.RemoveClaimAsync(role, permissionClaim);

        if (!addClaimIdentityResult.Succeeded)
        {
            throw new IdentityResultException(addClaimIdentityResult);
        }
    }
}
