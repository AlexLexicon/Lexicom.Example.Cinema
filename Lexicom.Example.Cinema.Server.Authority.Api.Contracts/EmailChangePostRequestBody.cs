namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
public class EmailChangePostRequestBody
{
    public required string NewEmail { get; set; }
    public required string EmailChangeToken { get; set; }
}
