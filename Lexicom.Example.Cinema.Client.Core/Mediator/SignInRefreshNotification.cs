using MediatR;

namespace Lexicom.Example.Cinema.Client.Core.Mediator;
public record class SignInRefreshNotification(string AccessToken, string RefreshToken) : INotification;