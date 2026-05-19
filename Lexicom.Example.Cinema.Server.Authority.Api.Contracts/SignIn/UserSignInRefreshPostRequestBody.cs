namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts.SignIn;
public class UserSignInRefreshPostRequestBody
{
    public required string AccessBearerToken { get; set; }
    public required string RefreshBearerToken { get; set; }
}
