using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;
using Lexicom.Example.Cinema.Server.Authority.Database.Entities;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Roles;

[TuiPage("Roles")]
public class CreateRole : ITuiOperation
{
    private readonly IComprehensiveService _comprehensiveService;
    private readonly IRoleService _roleService;

    public CreateRole(
        IComprehensiveService comprehensiveService,
        IRoleService roleService)
    {
        _comprehensiveService = comprehensiveService;
        _roleService = roleService;
    }

    public async Task ExecuteAsync()
    {
        IReadOnlyList<ComprehensiveRole> comprehensiveRoles = await _comprehensiveService.GetComprehensiveRolesAsync();
        Console.WriteLine("Current Roles:");
        Consolex.WriteAsJson(comprehensiveRoles);
        Console.WriteLine();

        string name = Consolex.ReadLine("Enter the name of the role you want to create");

        Role role = await _roleService.CreateRoleAsync(name);

        Console.WriteLine("New Role:");
        Consolex.WriteAsJson(role);
    }
}
