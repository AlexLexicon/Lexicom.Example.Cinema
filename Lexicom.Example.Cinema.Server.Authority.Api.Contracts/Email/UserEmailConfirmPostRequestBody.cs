namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts.Email;

public class UserEmailConfirmPostRequestBody
{
    public required string Email { get; set; }
    public required string EmailConfirmationToken { get; set; }
}
