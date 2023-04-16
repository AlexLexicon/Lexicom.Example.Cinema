using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Database;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Example.Cinema.Server.Shared.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Roles;
[TuiPage("Roles")]
public class AddPermissionToRole : ITuiOperation
{
    private readonly IDbContextFactory<AuthorityDbContext> _dbContextFactory;
    private readonly IRoleService _roleService;

    public AddPermissionToRole(
        IDbContextFactory<AuthorityDbContext> dbContextFactory,
        IRoleService roleService)
    {
        _dbContextFactory = dbContextFactory;
        _roleService = roleService;
    }

    public async Task ExecuteAsync()
    {
        using var db = await _dbContextFactory.CreateDbContextAsync();

        var aRoles = await db.Roles
            .Select(r => new
            {
                r.Id,
                r.Name,
            }).
            ToListAsync();

        Console.WriteLine("Roles:");
        Consolex.WriteAsJson(aRoles);

        Guid roleId = Consolex.ReadLineGuid("Enter the id of the role you want to add a permission to.");
        Console.WriteLine();
        Console.WriteLine();

        Console.WriteLine("Permissions:");
        Consolex.WriteAsJson(Policies.Permissions.All);

        Role role = await _roleService.GetRoleByIdAsync(roleId);

        IReadOnlyList<string> currerntRolePermissions = await _roleService.GetRolePermissionsAsync(roleId);

        Console.WriteLine("Current Role Permissions:");
        Consolex.WriteAsJson(currerntRolePermissions);
        Console.WriteLine();

        string permission = Consolex.ReadLine($"Enter the permission you want to add to the role '{role.Name}'");
        Console.WriteLine();
        Console.WriteLine();

        await _roleService.AddPermissionToRoleAsync(roleId, permission);

        IReadOnlyList<string> newRolePermissions = await _roleService.GetRolePermissionsAsync(roleId);

        Console.WriteLine("New Role Permissions:");
        Consolex.WriteAsJson(newRolePermissions);
    }
}
