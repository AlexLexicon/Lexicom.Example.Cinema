using MediatR;

namespace Lexicom.Example.Cinema.Client.Application.Mediator;
public record class MovieSearchResponseNotification(IReadOnlyList<MovieSearchResponseNotificationMovie> ResultsSlice, int TotalCount) : INotification;
