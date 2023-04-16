namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
public class SignInPostRequestBody
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}
