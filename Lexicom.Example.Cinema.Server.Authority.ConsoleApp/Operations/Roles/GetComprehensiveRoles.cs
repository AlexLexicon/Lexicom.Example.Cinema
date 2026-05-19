using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Models;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Roles;

[TuiPage("Roles")]
public class GetComprehensiveRoles : ITuiOperation
{
    private readonly IExtendedComprehensiveService _extendedComprehensiveService;

    public GetComprehensiveRoles(IExtendedComprehensiveService extendedComprehensiveService)
    {
        _extendedComprehensiveService = extendedComprehensiveService;
    }

    public async Task ExecuteAsync()
    {
        IReadOnlyList<ExtendedComprehensiveRole> extendedComprehensiveRoles = await _extendedComprehensiveService.GetExtendedComprehensiveRolesAsync();
        Console.WriteLine("Roles:");
        Consolex.WriteAsJson(extendedComprehensiveRoles);
    }
}
