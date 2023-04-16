namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
public class UserGetResponseBody
{
    public required Guid Id { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required DateTimeOffset CreatedDateTimeOffsetUtc { get; init; }
    public required List<UserGetResponseBodyRole> Roles { get; set; }
}
