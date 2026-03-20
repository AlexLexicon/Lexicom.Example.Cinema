using MediatR;

namespace Lexicom.Example.Cinema.Client.Core.Mediator;
public record class MovieSearchRequestNotification(int PageIndex, int PageLimit) : INotification;