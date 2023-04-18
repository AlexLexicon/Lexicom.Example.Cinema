using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Example.Cinema.Server.Shared.Authentication;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Seed;
[TuiPage("Seed")]
public class CreateBasicRole : ITuiOperation
{
    private readonly IRoleService _roleService;

    public CreateBasicRole(IRoleService roleService)
    {
        _roleService = roleService;
    }

    public async Task ExecuteAsync()
    {
        Role role = await _roleService.CreateRoleAsync(Shared.Authentication.Roles.BASIC_NAME);

        await _roleService.AddPermissionToRoleAsync(role.Id, Policies.Permissions.Authority.Password.PATCH);
        await _roleService.AddPermissionToRoleAsync(role.Id, Policies.Permissions.Authority.User.GET);
        await _roleService.AddPermissionToRoleAsync(role.Id, Policies.Permissions.Authority.User.PATCH);
        await _roleService.AddPermissionToRoleAsync(role.Id, Policies.Permissions.Authority.Email.EMAIL_CHANGE_POST);
        await _roleService.AddPermissionToRoleAsync(role.Id, Policies.Permissions.Authority.Email.EMAIL_CHANGE_CONFIRM_POST);

        ComprehensiveRole comprehensiveRole = await _roleService.GetComprehensiveRoleAsync(role.Id);

        Console.WriteLine("Role");
        Consolex.WriteAsJson(comprehensiveRole);
    }
}
