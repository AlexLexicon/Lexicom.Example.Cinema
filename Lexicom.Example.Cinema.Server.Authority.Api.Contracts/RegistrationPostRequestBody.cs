namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
public class RegistrationPostRequestBody
{
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
