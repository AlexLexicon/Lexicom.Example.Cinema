using MediatR;

namespace Lexicom.Example.Cinema.Client.Application.Mediator;
public record class ActorGetRequest(Guid ActorId) : IRequest<ActorGetResponse>;