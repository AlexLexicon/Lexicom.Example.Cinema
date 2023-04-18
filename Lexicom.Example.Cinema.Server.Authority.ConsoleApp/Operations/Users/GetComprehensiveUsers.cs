using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Users;
[TuiPage("Users")]
public class GetComprehensiveUsers : ITuiOperation
{
    private readonly IComprehensiveService _comprehensiveService;

    public GetComprehensiveUsers(IComprehensiveService comprehensiveService)
    {
        _comprehensiveService = comprehensiveService;
    }

    public async Task ExecuteAsync()
    {
        IReadOnlyList<ComprehensiveUser> comprehensiveUsers = await _comprehensiveService.GetComprehensiveUsersAsync();
        Console.WriteLine("Users:");
        Consolex.WriteAsJson(comprehensiveUsers);
    }
}
