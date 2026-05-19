using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Models;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Users;

[TuiPage("Users")]
public class Moderate : ITuiOperation
{
    private readonly IExtendedComprehensiveService _extendedComprehensiveService;
    private readonly IModerationService _moderationService;

    public Moderate(
        IExtendedComprehensiveService extendedComprehensiveService,
        IModerationService moderationService)
    {
        _extendedComprehensiveService = extendedComprehensiveService;
        _moderationService = moderationService;
    }

    public async Task ExecuteAsync()
    {
        IReadOnlyList<ExtendedComprehensiveUser> extendedComprehensiveUsers = await _extendedComprehensiveService.GetExtendedComprehensiveUsersAsync();
        Console.WriteLine("Available Users:");
        Consolex.WriteAsJson(extendedComprehensiveUsers);
        Console.WriteLine();

        Guid userId = Consolex.ReadLineGuid("Enter the id of the user you want to moderate:");
        Console.WriteLine();

        bool lockUser = Consolex
            .BinaryQuestion()
            .SetTrue("Lock User")
            .SetFalse("Unlock User")
            .Ask("What do you want to do?");
        Console.WriteLine();

        if (lockUser)
        {
            await _moderationService.LockUserAsync(userId);

            Console.WriteLine("User Locked Successfully.");
            Console.WriteLine();
        }
        else
        {
            await _moderationService.UnlockUserAsync(userId);

            Console.WriteLine("User Unlocked Successfully.");
            Console.WriteLine();
        }

        ExtendedComprehensiveUser updatedExtendedComprehensiveUser = await _extendedComprehensiveService.GetExtendedComprehensiveUserAsync(userId);
        Console.WriteLine("Updated User:");
        Consolex.WriteAsJson(updatedExtendedComprehensiveUser);
    }
}
