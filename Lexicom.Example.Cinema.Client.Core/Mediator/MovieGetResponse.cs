namespace Lexicom.Example.Cinema.Client.Core.Mediator;
public record class MovieGetResponse(string Title, TimeSpan Duration, DateTimeOffset ReleasedDateTimeOffsetUtc, string Synopsis);