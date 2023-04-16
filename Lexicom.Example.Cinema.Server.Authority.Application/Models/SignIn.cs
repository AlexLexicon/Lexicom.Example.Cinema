namespace Lexicom.Example.Cinema.Server.Authority.Application.Models;
public class SignIn
{
    public required string AccessToken { get; init; }
    public required string RefreshToken { get; init; }
}
