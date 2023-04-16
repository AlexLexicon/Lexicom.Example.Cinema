namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
public class PasswordPostRequestBody
{
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
}
