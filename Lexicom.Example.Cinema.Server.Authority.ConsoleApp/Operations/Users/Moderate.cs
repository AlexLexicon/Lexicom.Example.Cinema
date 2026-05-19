using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Users;

[TuiPage("Users")]
public class Moderate : ITuiOperation
{
    private readonly IComprehensiveService _comprehensiveService;
    private readonly IModerationService _moderationService;

    public Moderate(
        IComprehensiveService comprehensiveService,
        IModerationService moderationService)
    {
        _comprehensiveService = comprehensiveService;
        _moderationService = moderationService;
    }

    public async Task ExecuteAsync()
    {
        IReadOnlyList<ComprehensiveUser> comprehensiveUsers = await _comprehensiveService.GetComprehensiveUsersAsync();
        Console.WriteLine("Avaliable Users:");
        Consolex.WriteAsJson(comprehensiveUsers);
        Console.WriteLine();

        Guid userId = Consolex.ReadLineGuid("Enter the id of the user you want to moderate:");
        Console.WriteLine();

        bool lockUser = Consolex
            .BinaryQuestion()
            .SetTrue("Lock User")
            .SetFalse("UnLock User")
            .Ask("What do you want to do?");
        Console.WriteLine();

        if (lockUser)
        {
            await _moderationService.LockUserAsync(userId);
        }
        else
        {
            await _moderationService.UnlockUserAsync(userId);
        }
    }
}
