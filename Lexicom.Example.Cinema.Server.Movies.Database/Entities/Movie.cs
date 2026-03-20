namespace Lexicom.Example.Cinema.Server.Movies.Database.Entities;

public class Movie
{
    public required Guid Id { get; init; }

    public required string Title { get; set; }
    public required TimeSpan Duration { get; set; }
    public required DateTimeOffset ReleaseDateTimeOffsetUtc { get; set; }
    public required string Synopsis { get; set; }

    public required DateTimeOffset CreatedDateTimeOffsetUtc { get; init; }
    public required DateTimeOffset ModifiedDateTimeOffsetUtc { get; set; }
}
