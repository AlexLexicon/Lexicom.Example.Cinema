namespace Lexicom.Example.Cinema.Server.Movies.Api.Contracts.Review;
public class ReviewPostRequestBody
{
    public required int Rating { get; set; }
    public string? Text { get; set; }
}
