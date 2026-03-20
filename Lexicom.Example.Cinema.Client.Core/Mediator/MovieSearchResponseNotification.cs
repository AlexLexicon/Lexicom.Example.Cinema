using MediatR;

namespace Lexicom.Example.Cinema.Client.Core.Mediator;
public record class MovieSearchResponseNotification(IReadOnlyList<MovieSearchResponseNotificationMovie> ResultsSlice, int TotalCount) : INotification;
