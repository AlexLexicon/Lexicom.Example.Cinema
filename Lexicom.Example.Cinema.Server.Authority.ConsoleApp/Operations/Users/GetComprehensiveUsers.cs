using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Models;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Users;

[TuiPage("Users")]
public class GetComprehensiveUsers : ITuiOperation
{
    private readonly IExtendedComprehensiveService _extendedComprehensiveService;

    public GetComprehensiveUsers(IExtendedComprehensiveService extendedComprehensiveService)
    {
        _extendedComprehensiveService = extendedComprehensiveService;
    }

    public async Task ExecuteAsync()
    {
        IReadOnlyList<ExtendedComprehensiveUser> extendedComprehensiveUsers = await _extendedComprehensiveService.GetExtendedComprehensiveUsersAsync();
        Console.WriteLine("Users:");
        Consolex.WriteAsJson(extendedComprehensiveUsers);
    }
}
