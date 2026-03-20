using MediatR;

namespace Lexicom.Example.Cinema.Client.Core.Mediator;
public record class DirectorGetRequest(Guid DirectorId) : IRequest<DirectorGetResponse>;