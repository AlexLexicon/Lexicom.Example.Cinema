using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Models;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Users;

[TuiPage("Users")]
public class AddRoleToUser : ITuiOperation
{
    private readonly IExtendedComprehensiveService _extendedComprehensiveService;
    private readonly IUserService _userService;

    public AddRoleToUser(
        IExtendedComprehensiveService extendedComprehensiveService,
        IUserService userService)
    {
        _extendedComprehensiveService = extendedComprehensiveService;
        _userService = userService;
    }

    public async Task ExecuteAsync()
    {
        IReadOnlyList<ExtendedComprehensiveUser> extendedComprehensiveUsers = await _extendedComprehensiveService.GetExtendedComprehensiveUsersAsync();
        Console.WriteLine("Avaliable Users:");
        Consolex.WriteAsJson(extendedComprehensiveUsers);
        Console.WriteLine();

        Guid userId = Consolex.ReadLineGuid("Enter the id of the user you want to add a role to:");
        Console.WriteLine();

        IReadOnlyList<ExtendedComprehensiveRole> extendedComprehensiveRoles = await _extendedComprehensiveService.GetExtendedComprehensiveRolesAsync();
        Console.WriteLine("Avaliable Roles:");
        Consolex.WriteAsJson(extendedComprehensiveRoles);
        Console.WriteLine();

        Guid roleId = Consolex.ReadLineGuid("Enter the id of the role you want to add to the user:");
        Console.WriteLine();

        await _userService.AddRoleToUserAsync(userId, roleId);

        ExtendedComprehensiveUser updatedExtendedComprehensiveUser = await _extendedComprehensiveService.GetExtendedComprehensiveUserAsync(userId);
        Console.WriteLine("Updated User:");
        Consolex.WriteAsJson(updatedExtendedComprehensiveUser);
    }
}
