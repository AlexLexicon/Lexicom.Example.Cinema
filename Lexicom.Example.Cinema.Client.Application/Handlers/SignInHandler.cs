using CommunityToolkit.Mvvm.Messaging;
using Lexicom.AspNetCore.Controllers.Contracts;
using Lexicom.AspNetCore.Controllers.Contracts.Extensions;
using Lexicom.Concentrate.Client.Authentication;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Options;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts.SignIn;
using Lexicom.Http.Extensions;
using Lexicom.Mvvm;
using Lexicom.Mvvm.Extensions;
using System.Net.Http.Json;

namespace Lexicom.Example.Cinema.Client.Application.Handlers;
public class SignInHandler : IAsyncRecipient<SignInNotification>
{
    private readonly IMessenger _messenger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IAuthenticationTokenStore _authenticationTokenStore;

    public SignInHandler(
        IMessenger messenger,
        IHttpClientFactory httpClientFactory,
        IAuthenticationTokenStore authenticationService)
    {
        _messenger = messenger;
        _httpClientFactory = httpClientFactory;
        _authenticationTokenStore = authenticationService;
    }

    public async Task ReceiveAsync(SignInNotification message, CancellationToken cancellationToken)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient(nameof(HttpClientAuthorityAnonymousApiOptions));

        HttpResponseMessage response = await httpClient.PostAsJsonAsync("user/signin", new UserSignInPostRequestBody
        {
            Email = message.Email,
            Password = message.Password,
        }, cancellationToken);

        ErrorResponse? errorResponse = await response.TryToErrorResponseAsync();

        if (errorResponse.HasCode(AuthorityErrorCodes.USER_CREDENTIALS_INCORRECT))
        {
            await _messenger.SendAsync(new SignInFailedNotification(SignInFailedNotification.Errors.IncorrectCredentials), cancellationToken);
            return;
        }

        if (errorResponse.HasCode(AuthorityErrorCodes.USER_MODERATION_LOCKED))
        {
            await _messenger.SendAsync(new SignInFailedNotification(SignInFailedNotification.Errors.LockedOut), cancellationToken);
            return;
        }

        if (errorResponse.HasCode(AuthorityErrorCodes.USER_VERIFICATION_INCOMPLETE))
        {
            await _messenger.SendAsync(new SignInFailedNotification(SignInFailedNotification.Errors.NotVerified), cancellationToken);
            return;
        }

        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadFromJsonNotNullAsync<UserSignInPostResponseBody>(cancellationToken: cancellationToken);

        string accessToken = responseBody.AccessBearerToken;
        string refreshToken = responseBody.RefreshBearerToken;

        var setAccessTokenTask = _authenticationTokenStore.SetAccessTokenAsync(accessToken);
        var setRefreshTokenTask = _authenticationTokenStore.SetRefreshTokenAsync(refreshToken);

        await setAccessTokenTask;
        await setRefreshTokenTask;

        await _messenger.SendAsync(new SignInSuccessNotification(), cancellationToken);
    }
}
