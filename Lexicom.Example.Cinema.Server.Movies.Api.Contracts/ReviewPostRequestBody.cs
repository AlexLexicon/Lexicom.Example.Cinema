namespace Lexicom.Example.Cinema.Server.Movies.Api.Contracts;
public class ReviewPostRequestBody
{
    public required int Rating { get; set; }
    public string? Text { get; set; }
}
