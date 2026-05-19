namespace Lexicom.Example.Cinema.Server.Authority.Application.Models;
public class ComprehensiveUser
{
    public required Guid Id { get; init; }
    public required string Email { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required DateTimeOffset CreatedDateTimeOffsetUtc { get; init; }
    public required DateTimeOffset? VerifiedDateTimeOffsetUtc { get; init; }
    public required DateTimeOffset? LastSignInDateTimeOffsetUtc { get; init; }
    public required DateTimeOffset? LockoutEndDateTimeOffsetUtc { get; init; }
    public required IReadOnlyList<ComprehensiveRole> Roles { get; init; }
}
