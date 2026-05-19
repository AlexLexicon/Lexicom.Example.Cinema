namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts.SignIn;
public class UserSignInPostResponseBody
{
    public required string AccessBearerToken { get; set; }
    public required string RefreshBearerToken { get; set; }
}