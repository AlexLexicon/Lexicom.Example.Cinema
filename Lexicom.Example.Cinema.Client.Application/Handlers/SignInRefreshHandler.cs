using Lexicom.Concentrate.Client.Authentication;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Options;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts.SignIn;
using Lexicom.Http.Extensions;
using Lexicom.Mvvm;
using System.Net.Http.Json;

namespace Lexicom.Example.Cinema.Client.Application.Handlers;
public class SignInRefreshHandler : IAsyncRecipient<SignInRefreshNotification>
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IAuthenticationTokenStore _authenticationTokenStore;

    public SignInRefreshHandler(
        IHttpClientFactory httpClientFactory,
        IAuthenticationTokenStore authenticationService)
    {
        _httpClientFactory = httpClientFactory;
        _authenticationTokenStore = authenticationService;
    }

    public async Task ReceiveAsync(SignInRefreshNotification message, CancellationToken cancellationToken)
    {
        var setAccessTokenTask = _authenticationTokenStore.SetAccessTokenAsync(null);
        var setRefreshTokenTask = _authenticationTokenStore.SetRefreshTokenAsync(null);

        await setAccessTokenTask;
        await setRefreshTokenTask;

        HttpClient httpClient = _httpClientFactory.CreateClient(nameof(HttpClientAuthorityAnonymousApiOptions));

        HttpResponseMessage response = await httpClient.PostAsJsonAsync("user/signin/refresh", new UserSignInRefreshPostRequestBody
        {
            AccessBearerToken = message.AccessToken,
            RefreshBearerToken = message.RefreshToken,
        }, cancellationToken);

        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadFromJsonNotNullAsync<UserSignInRefreshPostResponseBody>(cancellationToken: cancellationToken);

        string newAccessToken = responseBody.AccessBearerToken;
        string newRefreshToken = responseBody.RefreshBearerToken;

        setAccessTokenTask = _authenticationTokenStore.SetAccessTokenAsync(newAccessToken);
        setRefreshTokenTask = _authenticationTokenStore.SetRefreshTokenAsync(newRefreshToken);

        await setAccessTokenTask;
        await setRefreshTokenTask;
    }
}
