namespace Lexicom.Example.Cinema.Shared.Models;

public class MovieData
{
    public required string Title { get; init; }
    public required TimeSpan Duration { get; init; }
    public required DateTimeOffset ReleaseDateTimeOffsetUtc { get; init; }
    public required string Synopsis { get; init; }
}
