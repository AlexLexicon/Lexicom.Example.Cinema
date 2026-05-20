using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Options;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts.User;
using Lexicom.Http.Extensions;

namespace Lexicom.Example.Cinema.Client.Application.Handlers;
public class UserGetHandler
{
    private readonly IHttpClientFactory _httpClientFactory;

    public UserGetHandler(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<UserGetResponse> Handle(UserGetRequest request, CancellationToken cancellationToken)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient(nameof(HttpClientAuthorityApiOptions));

        var responseBody = await httpClient.GetFromJsonNotNullAsync<UserGetResponseBody>("user", cancellationToken);

        return new UserGetResponse(responseBody);
    }
}
