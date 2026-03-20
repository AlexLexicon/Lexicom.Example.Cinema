using MediatR;

namespace Lexicom.Example.Cinema.Client.Core.Mediator;
public record class SignInNotification(string Email, string Password) : INotification;
