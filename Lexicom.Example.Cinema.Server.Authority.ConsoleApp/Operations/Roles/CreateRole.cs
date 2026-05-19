using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Models;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;
using Lexicom.Example.Cinema.Server.Authority.Database.Entities;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Roles;

[TuiPage("Roles")]
public class CreateRole : ITuiOperation
{
    private readonly IExtendedComprehensiveService _extendedComprehensiveService;
    private readonly IRoleService _roleService;

    public CreateRole(
        IExtendedComprehensiveService extendedComprehensiveService,
        IRoleService roleService)
    {
        _extendedComprehensiveService = extendedComprehensiveService;
        _roleService = roleService;
    }

    public async Task ExecuteAsync()
    {
        IReadOnlyList<ExtendedComprehensiveRole> extendedComprehensiveRoles = await _extendedComprehensiveService.GetExtendedComprehensiveRolesAsync();
        Console.WriteLine("Current Roles:");
        Consolex.WriteAsJson(extendedComprehensiveRoles);
        Console.WriteLine();

        string name = Consolex.ReadLine("Enter the name of the role you want to create");

        Role role = await _roleService.CreateRoleAsync(name);

        ExtendedComprehensiveRole extendedComprehensiveRole = await _extendedComprehensiveService.GetExtendedComprehensiveRoleAsync(role.Id);

        Console.WriteLine("New Role:");
        Consolex.WriteAsJson(extendedComprehensiveRole);
    }
}
