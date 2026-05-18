namespace Lexicom.Example.Cinema.Server.Authority.Application.Models;
public class ComprehensiveUser
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string WhenVerified { get; init; }
    public required string WhenLastSignIn { get; init; }
    public required string WhenCreated { get; init; }
    public required IReadOnlyList<ComprehensiveRole> Roles { get; init; }
}
