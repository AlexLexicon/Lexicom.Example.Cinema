namespace Lexicom.Example.Cinema.Server.Movies.Api.Contracts;

public class ReviewGetSummaryResponseBody
{
    public required double AverageRating { get; set; }
    public required Dictionary<int, int> RatingsDistribution { get; set; }
}
