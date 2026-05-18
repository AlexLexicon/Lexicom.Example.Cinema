using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Authentication.Http;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Mvvm.Extensions;

namespace Lexicom.Example.Cinema.Client.Application.Services;

public class HttpClientUnathorizedListener : IHttpClientUnauthorizedListener
{
    private readonly IMessenger _messenger;

    public HttpClientUnathorizedListener(IMessenger messenger)
    {
        _messenger = messenger;
    }

    public async Task UnauthorizedAsync()
    {
        await _messenger.SendAsync(new UnathorizedNotification());
    }
}
