using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Models;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;
using Lexicom.Example.Cinema.Server.Shared.Authentication;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Roles;

[TuiPage("Roles")]
public class AddPermissionToRole : ITuiOperation
{
    private readonly IExtendedComprehensiveService _extendedComprehensiveService;
    private readonly IRoleService _roleService;

    public AddPermissionToRole(
        IRoleService roleService,
        IExtendedComprehensiveService extendedComprehensiveService)
    {
        _roleService = roleService;
        _extendedComprehensiveService = extendedComprehensiveService;
    }

    public async Task ExecuteAsync()
    {
        IReadOnlyList<ExtendedComprehensiveRole> extendedComprehensiveRoles = await _extendedComprehensiveService.GetExtendedComprehensiveRolesAsync();
        Console.WriteLine("Avaliable Roles:");
        Consolex.WriteAsJson(extendedComprehensiveRoles);
        Console.WriteLine();

        Guid roleId = Consolex.ReadLineGuid("Enter the id of the role you want to add a permission to:");
        Console.WriteLine();

        Console.WriteLine("Avaliable Permissions:");
        Consolex.WriteAsJson(Policies.Permissions.All);
        Console.WriteLine();

        string permission = Consolex.ReadLine($"Enter the permission you want to add to the role:");
        Console.WriteLine();

        await _roleService.AddPermissionToRoleAsync(roleId, permission);

        ExtendedComprehensiveRole updatedExtendedComprehensiveRole = await _extendedComprehensiveService.GetExtendedComprehensiveRoleAsync(roleId);
        Console.WriteLine("Updated Role:");
        Consolex.WriteAsJson(updatedExtendedComprehensiveRole);
    }
}
