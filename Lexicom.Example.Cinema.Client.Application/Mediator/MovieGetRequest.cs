using MediatR;

namespace Lexicom.Example.Cinema.Client.Application.Mediator;
public record class MovieGetRequest(Guid MovieId) : IRequest<MovieGetResponse>;