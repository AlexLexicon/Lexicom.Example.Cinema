using MediatR;

namespace Lexicom.Example.Cinema.Client.Core.Mediator;
public record class UserGetRequest() : IRequest<UserGetResponse>;
