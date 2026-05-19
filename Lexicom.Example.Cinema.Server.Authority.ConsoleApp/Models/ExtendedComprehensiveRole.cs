using Lexicom.Example.Cinema.Server.Authority.Application.Models;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Models;

public class ExtendedComprehensiveRole : ComprehensiveRole
{
    public required string WhenCreated { get; init; }
}
