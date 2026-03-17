namespace Lexicom.Example.Cinema.Server.Movies.ConsoleApp.Models;
public class JsonMovie
{
    public required string Title { get; init; }
    public required TimeSpan Duration { get; init; }
    public required DateTimeOffset ReleaseDateTimeOffsetUtc { get; init; }
    public required string Synopsis { get; init; }
}
