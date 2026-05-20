namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts.Password;

public class UserPasswordPostRequestBody
{
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
}
