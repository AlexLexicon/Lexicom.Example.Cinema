namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts.Registration;

public class UserRegistrationPostRequestBody
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
