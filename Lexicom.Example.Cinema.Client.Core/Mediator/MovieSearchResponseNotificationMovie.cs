namespace Lexicom.Example.Cinema.Client.Core.Mediator;
public record class MovieSearchResponseNotificationMovie(Guid Id, string Title, DateTimeOffset ReleasedDateTimeOffsetUtc, TimeSpan Duration, string Synopsis);
