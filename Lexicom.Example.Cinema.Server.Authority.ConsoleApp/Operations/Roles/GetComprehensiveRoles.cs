using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Roles;
[TuiPage("Roles")]
public class GetComprehensiveRoles : ITuiOperation
{
    private readonly IComprehensiveService _comprehensiveService;

    public GetComprehensiveRoles(IComprehensiveService comprehensiveService)
    {
        _comprehensiveService = comprehensiveService;
    }

    public async Task ExecuteAsync()
    {
        IReadOnlyList<ComprehensiveRole> comprehensiveRoles = await _comprehensiveService.GetComprehensiveRolesAsync();
        Console.WriteLine("Roles:");
        Consolex.WriteAsJson(comprehensiveRoles);
    }
}
