using MediatR;

namespace Lexicom.Example.Cinema.Client.Application.Mediator;
public record class MovieSearchRequestNotification(int PageIndex, int PageLimit) : INotification;