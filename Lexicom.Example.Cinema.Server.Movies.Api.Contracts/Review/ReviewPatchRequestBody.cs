namespace Lexicom.Example.Cinema.Server.Movies.Api.Contracts.Review;

public class ReviewPatchRequestBody
{
    public int? NewRating { get; set; }
    public string? NewText { get; set; }
}

