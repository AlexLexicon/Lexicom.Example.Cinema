using MediatR;

namespace Lexicom.Example.Cinema.Client.Application.Mediator;
public record class SignInNotification(string Email, string Password) : INotification;
