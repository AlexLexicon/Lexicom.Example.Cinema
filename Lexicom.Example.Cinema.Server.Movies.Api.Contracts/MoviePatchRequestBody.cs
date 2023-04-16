namespace Lexicom.Example.Cinema.Server.Movies.Api.Contracts;
public class MoviePatchRequestBody
{
    public string? NewTitle { get; set; }
    public TimeSpan? NewDuration { get; set; }
    public DateTimeOffset? NewReleaseDateTimeOffsetUtc { get; set; }
    public string? NewSynopsis { get; set; }
}
