using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Users;
[TuiPage("Users")]
public class AddRoleToUser : ITuiOperation
{
    private readonly IComprehensiveService _comprehensiveService;
    private readonly IUserService _userService;

    public AddRoleToUser(
        IComprehensiveService comprehensiveService,
        IUserService userService)
    {
        _comprehensiveService = comprehensiveService;
        _userService = userService;
    }

    public async Task ExecuteAsync()
    {
        IReadOnlyList<ComprehensiveUser> comprehensiveUsers = await _comprehensiveService.GetComprehensiveUsersAsync();
        Console.WriteLine("Avaliable Users:");
        Consolex.WriteAsJson(comprehensiveUsers);
        Console.WriteLine();

        Guid userId = Consolex.ReadLineGuid("Enter the id of the user you want to add a role to:");
        Console.WriteLine();

        IReadOnlyList<ComprehensiveRole> comprehensiveRoles = await _comprehensiveService.GetComprehensiveRolesAsync();
        Console.WriteLine("Avaliable Roles:");
        Consolex.WriteAsJson(comprehensiveRoles);
        Console.WriteLine();

        Guid roleId = Consolex.ReadLineGuid("Enter the id of the role you want to add to the user:");
        Console.WriteLine();

        await _userService.AddRoleToUserAsync(userId, roleId);

        ComprehensiveUser updatedComprehensiveUser = await _userService.GetComprehensiveUserAsync(userId);
        Console.WriteLine("Updated User:");
        Consolex.WriteAsJson(updatedComprehensiveUser);
    }
}
