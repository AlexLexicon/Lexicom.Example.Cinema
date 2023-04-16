namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
public class VerificationEmailConfirmPostRequestBody
{
    public required string Email { get; set; }
    public required string EmailConfirmationToken { get; set; }
}
