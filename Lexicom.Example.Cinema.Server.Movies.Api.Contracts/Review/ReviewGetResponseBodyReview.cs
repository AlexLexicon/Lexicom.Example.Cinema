namespace Lexicom.Example.Cinema.Server.Movies.Api.Contracts.Review;

public class ReviewGetResponseBodyReview
{
    public required Guid Id { get; set; }
    public required int Rating { get; set; }
    public required string? Text { get; set; }
    public required DateTimeOffset PostedDateTimeOffsetUtc { get; set; }
}
