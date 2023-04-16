namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
public class VerificationEmailChangePostRequestBody
{
    public required string NewEmail { get; set; }
    public required string EmailChangeToken { get; set; }
}
