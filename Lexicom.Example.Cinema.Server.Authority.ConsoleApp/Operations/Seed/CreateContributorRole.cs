using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Models;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;
using Lexicom.Example.Cinema.Server.Authority.Database.Entities;
using Lexicom.Example.Cinema.Server.Shared.Authentication;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Seed;

[TuiPage("Seed")]
public class CreateContributorRole : ITuiOperation
{
    private readonly IRoleService _roleService;
    private readonly IExtendedComprehensiveService _extendedComprehensiveService;

    public CreateContributorRole(
        IRoleService roleService,
        IExtendedComprehensiveService extendedComprehensiveService)
    {
        _roleService = roleService;
        _extendedComprehensiveService = extendedComprehensiveService;
    }

    public async Task ExecuteAsync()
    {
        string roleName = Shared.Authentication.Roles.CONTRIBUTOR_NAME;

        Role role = await _roleService.CreateRoleAsync(roleName);

        await _roleService.AddPermissionToRoleAsync(role.Id, Policies.Permissions.Movies.Movie.PATCH);
        await _roleService.AddPermissionToRoleAsync(role.Id, Policies.Permissions.Movies.Movie.POST);

        ExtendedComprehensiveRole extendedComprehensiveRole = await _extendedComprehensiveService.GetExtendedComprehensiveRoleAsync(role.Id);
        Console.WriteLine($"'{roleName}' Role");
        Consolex.WriteAsJson(extendedComprehensiveRole);
    }
}
