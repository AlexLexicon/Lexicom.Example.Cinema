using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Database;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;
using Lexicom.Example.Cinema.Server.Shared.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Roles;
[TuiPage("Roles")]
public class AddPermissionToRole : ITuiOperation
{
    private readonly IComprehensiveService _comprehensiveService;
    private readonly IDbContextFactory<AuthorityDbContext> _dbContextFactory;
    private readonly IRoleService _roleService;

    public AddPermissionToRole(
        IDbContextFactory<AuthorityDbContext> dbContextFactory,
        IRoleService roleService,
        IComprehensiveService comprehensiveService)
    {
        _dbContextFactory = dbContextFactory;
        _roleService = roleService;
        _comprehensiveService = comprehensiveService;
    }

    public async Task ExecuteAsync()
    {
        IReadOnlyList<ComprehensiveRole> comprehensiveRoles = await _comprehensiveService.GetComprehensiveRolesAsync();
        Console.WriteLine("Avaliable Roles:");
        Consolex.WriteAsJson(comprehensiveRoles);
        Console.WriteLine();

        Guid roleId = Consolex.ReadLineGuid("Enter the id of the role you want to add a permission to:");
        Console.WriteLine();

        Console.WriteLine("Avaliable Permissions:");
        Consolex.WriteAsJson(Policies.Permissions.All);
        Console.WriteLine();

        string permission = Consolex.ReadLine($"Enter the permission you want to add to the role:");
        Console.WriteLine();

        await _roleService.AddPermissionToRoleAsync(roleId, permission);

        ComprehensiveRole updatedComprehensiveRole = await _roleService.GetComprehensiveRoleAsync(roleId);
        Console.WriteLine("Updated Role:");
        Consolex.WriteAsJson(updatedComprehensiveRole);
    }
}
