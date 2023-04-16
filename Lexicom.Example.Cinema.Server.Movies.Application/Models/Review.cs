namespace Lexicom.Example.Cinema.Server.Movies.Application.Models;
public class Review
{
    public required Guid Id { get; set; }
    public required Guid MovieId { get; set; }
    public required Guid UserId { get; set; }
    public required int ScoreOverFive { get; set; }
    public required string? Text { get; set; }

    public required DateTimeOffset CreatedDateTimeOffsetUtc { get; set; }
    public required DateTimeOffset ModifiedDateTimeOffsetUtc { get; set; }
}
