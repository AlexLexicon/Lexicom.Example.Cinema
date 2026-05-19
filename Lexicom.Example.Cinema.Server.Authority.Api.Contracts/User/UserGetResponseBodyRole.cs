namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts.User;
public class UserGetResponseBodyRole
{
    public required Guid Id { get; set; }
    public required string Name { get; set; }
    public required List<string> Permissions { get; set; }
}
