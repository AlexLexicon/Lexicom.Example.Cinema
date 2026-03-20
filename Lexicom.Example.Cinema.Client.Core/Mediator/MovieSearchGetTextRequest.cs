using MediatR;

namespace Lexicom.Example.Cinema.Client.Core.Mediator;
public record class MovieSearchGetTextRequest : IRequest<MovieSearchGetTextResponse>;