namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts.User;

public class UserGetResponseBody
{
    public required Guid Id { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTimeOffset CreatedDateTimeOffsetUtc { get; set; }
    public required List<UserGetResponseBodyRole> Roles { get; set; }
}
