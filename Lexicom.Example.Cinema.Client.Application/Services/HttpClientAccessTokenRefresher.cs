using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Authentication.Http;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Mvvm.Extensions;

namespace Lexicom.Example.Cinema.Client.Application.Services;
public class HttpClientAccessTokenRefresher : IHttpClientAccessTokenRefresher
{
    private readonly IMessenger _messenger;

    public HttpClientAccessTokenRefresher(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public async Task RefreshAuthenticationAsync(string? accessToken, string? refreshToken)
    {
        ArgumentNullException.ThrowIfNull(accessToken);
        ArgumentNullException.ThrowIfNull(refreshToken);

        await _messenger.SendAsync(new SignInRefreshNotification(accessToken, refreshToken));
    }
}
