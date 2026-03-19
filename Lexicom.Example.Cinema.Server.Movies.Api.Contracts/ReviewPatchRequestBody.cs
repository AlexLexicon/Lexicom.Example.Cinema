namespace Lexicom.Example.Cinema.Server.Movies.Api.Contracts;

public class ReviewPatchRequestBody
{
    public int? NewRating { get; set; }
    public string? NewText { get; set; }
}

