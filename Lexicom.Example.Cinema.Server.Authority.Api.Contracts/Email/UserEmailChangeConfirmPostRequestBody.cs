namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts.Email;

public class UserEmailChangeConfirmPostRequestBody
{
    public required string NewEmail { get; set; }
    public required string EmailChangeToken { get; set; }
}
