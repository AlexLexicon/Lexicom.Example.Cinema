using MediatR;

namespace Lexicom.Example.Cinema.Client.Application.Mediator;
public record class UserGetRequest() : IRequest<UserGetResponse>;
