using MediatR;

namespace Lexicom.Example.Cinema.Client.Application.Mediator;
public record class SignInRefreshNotification(string AccessToken, string RefreshToken) : INotification;