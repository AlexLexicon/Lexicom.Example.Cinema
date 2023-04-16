using Lexicom.Authentication.Http;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Application.Services;
public class HttpClientAccessTokenRefresher : IHttpClientAccessTokenRefresher
{
    private readonly IMediator _mediator;

    public HttpClientAccessTokenRefresher(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task RefreshAuthenticationAsync(string? accessToken, string? refreshToken)
    {
        ArgumentNullException.ThrowIfNull(accessToken);
        ArgumentNullException.ThrowIfNull(refreshToken);

        await _mediator.Publish(new SignInRefreshNotification(accessToken, refreshToken));
    }
}
