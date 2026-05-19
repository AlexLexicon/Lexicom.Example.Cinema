namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts.Password;
public class UserPasswordForgotResetPostRequestBody
{
    public required string Email { get; set; }
    public required string PasswordResetToken { get; set; }
    public required string NewPassword { get; set; }
}
