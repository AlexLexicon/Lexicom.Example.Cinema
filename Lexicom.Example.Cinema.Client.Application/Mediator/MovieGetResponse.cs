namespace Lexicom.Example.Cinema.Client.Application.Mediator;
public record class MovieGetResponse(string Title, TimeSpan Duration, DateTimeOffset ReleasedDateTimeOffsetUtc, string Synopsis);