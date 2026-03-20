using Lexicom.Authentication.Http;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Application.Services;
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
