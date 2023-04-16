namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
public class SignInPostResponseBody
{
    public required string AccessBearerToken { get; set; }
    public required string RefreshBearerToken { get; set; }
}