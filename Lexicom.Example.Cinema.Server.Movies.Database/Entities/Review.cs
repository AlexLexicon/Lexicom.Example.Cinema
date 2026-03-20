namespace Lexicom.Example.Cinema.Server.Movies.Database.Entities;

public class Review
{
    public required Guid Id { get; init; }
    public required Guid MovieId { get; init; }
    public required Guid UserId { get; init; }

    public required int Rating { get; set; }
    public required string? Text { get; set; }

    public required DateTimeOffset CreatedDateTimeOffsetUtc { get; init; }
    public required DateTimeOffset ModifiedDateTimeOffsetUtc { get; set; }
}
