namespace Lexicom.Example.Cinema.Server.Movies.Application.Models;
public class Movie
{
    public required Guid Id { get; set; }
    public required string Title { get; set; }
    public required TimeSpan Duration { get; set; }
    public required DateTimeOffset ReleaseDateTimeOffsetUtc { get; set; }
    public required string Synopsis { get; set; }

    public required DateTimeOffset CreatedDateTimeOffsetUtc { get; set; }
    public required DateTimeOffset ModifiedDateTimeOffsetUtc { get; set; }
}
