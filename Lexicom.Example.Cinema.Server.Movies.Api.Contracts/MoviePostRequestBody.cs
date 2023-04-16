namespace Lexicom.Example.Cinema.Server.Movies.Api.Contracts;
public class MoviePostRequestBody
{
    public required string Title { get; set; }
    public required TimeSpan Duration { get; set; }
    public required DateTimeOffset ReleaseDateTimeOffsetUtc { get; set; }
    public required string Synopsis { get; set; }
}
