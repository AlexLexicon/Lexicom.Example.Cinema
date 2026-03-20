using Lexicom.Authentication.Http;
using Lexicom.Example.Cinema.Client.Core.Mediator;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Core.Services;
public class HttpClientUnathorizedListener : IHttpClientUnathorizedListener
{
    private readonly IMediator _mediator;

    public HttpClientUnathorizedListener(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task UnathorizedAsync()
    {
        await _mediator.Publish(new UnathorizedNotification());
    }
}
