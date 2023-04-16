namespace Lexicom.Example.Cinema.Server.Movies.Api.Contracts;
public class RatingPostRequestBody
{
    public required double Rating { get; set; }
    public string? Text { get; set; }
}
