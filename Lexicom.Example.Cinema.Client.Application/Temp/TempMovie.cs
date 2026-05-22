namespace Lexicom.Example.Cinema.Client.Application.Temp;

public class TempMovie
{
    public required Guid Id { get; init; }
    public required string Title { get; init; }
    public required TimeSpan Duration { get; init; }
    public required DateTimeOffset ReleaseDateTimeOffsetUtc { get; init; }
    public required string Synopsis { get; init; }
}
