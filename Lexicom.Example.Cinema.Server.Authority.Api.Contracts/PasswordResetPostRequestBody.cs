namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
public class PasswordResetPostRequestBody
{
    public required string Email { get; set; }
    public required string PasswordResetToken { get; set; }
    public required string NewPassword { get; set; }
}
