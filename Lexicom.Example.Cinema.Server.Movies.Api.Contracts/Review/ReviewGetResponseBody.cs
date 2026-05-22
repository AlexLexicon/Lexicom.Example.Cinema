namespace Lexicom.Example.Cinema.Server.Movies.Api.Contracts.Review;

public class ReviewGetResponseBody
{
    public required int TotalReviews { get; set; }
    public required List<ReviewGetResponseBodyReview> Reviews { get; set; }
}
