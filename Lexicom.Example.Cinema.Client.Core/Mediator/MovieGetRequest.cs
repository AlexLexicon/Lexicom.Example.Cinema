using MediatR;

namespace Lexicom.Example.Cinema.Client.Core.Mediator;
public record class MovieGetRequest(Guid MovieId) : IRequest<MovieGetResponse>;