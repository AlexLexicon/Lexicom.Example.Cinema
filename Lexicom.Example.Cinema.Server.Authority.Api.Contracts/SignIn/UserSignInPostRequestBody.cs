namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts.SignIn;

public class UserSignInPostRequestBody
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
