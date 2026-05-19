using Lexicom.Example.Cinema.Server.Authority.Application.Models;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Models;

public class ExtendedComprehensiveUser : ComprehensiveUser
{
    public required string WhenVerified { get; init; }
    public required string WhenLastSignIn { get; init; }
    public required string WhenCreated { get; init; }
    public required string LockedOutUntil { get; init; }
}
