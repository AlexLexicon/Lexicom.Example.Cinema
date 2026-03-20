using MediatR;

namespace Lexicom.Example.Cinema.Client.Application.Mediator;
public record class DirectorGetRequest(Guid DirectorId) : IRequest<DirectorGetResponse>;