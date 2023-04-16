namespace Lexicom.Example.Cinema.Server.Authority.Application.Models;
public class ComprehensiveUser
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required DateTimeOffset CreatedDateTimeOffset { get; init; }
    public required DateTimeOffset? VerifiedDateTimeOffset { get; init; }
    public required DateTimeOffset? LastSignInDateTimeOffset { get; init; }
    public required IReadOnlyList<ComprehensiveUserRole> Roles { get; init; }
}
