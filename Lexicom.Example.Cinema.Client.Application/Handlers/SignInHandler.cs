using Lexicom.AspNetCore.Controllers.Contracts;
using Lexicom.AspNetCore.Controllers.Contracts.Extensions;
using Lexicom.Concentrate.Client.Authentication;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Options;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts.Errors;
using Lexicom.Http.Extensions;
using MediatR;
using System.Net.Http.Json;

namespace Lexicom.Example.Cinema.Client.Application.Handlers;
public class SignInHandler : INotificationHandler<SignInNotification>
{
    private readonly IMediator _mediator;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IAuthenticationTokenStore _authenticationTokenStore;

    public SignInHandler(
        IMediator mediator,
        IHttpClientFactory httpClientFactory,
        IAuthenticationTokenStore authenticationService)
    {
        _mediator = mediator;
        _httpClientFactory = httpClientFactory;
        _authenticationTokenStore = authenticationService;
    }

    public async Task Handle(SignInNotification request, CancellationToken cancellationToken)
    {
        HttpClient httpClient = _httpClientFactory.CreateClient(nameof(HttpClientAuthorityAnonymousApiOptions));

        HttpResponseMessage response = await httpClient.PostAsJsonAsync("user/signin", new SignInPostRequestBody
        {
            Email = request.Email,
            Password = request.Password,
        }, cancellationToken);

        ErrorResponse? errorResponse = await response.TryToErrorResponseAsync();

        if (errorResponse.HasCode(AuthorityErrorCodes.USER_CREDENTIALS_INCORRECT))
        {
            await _mediator.Publish(new SignInFailedNotification(SignInFailedNotification.Errors.IncorrectCredentials), cancellationToken);
            return;
        }

        if (errorResponse.HasCode(AuthorityErrorCodes.USER_MODERATION_LOCKED))
        {
            await _mediator.Publish(new SignInFailedNotification(SignInFailedNotification.Errors.LockedOut), cancellationToken);
            return;
        }

        if (errorResponse.HasCode(AuthorityErrorCodes.USER_VERIFICATION_INCOMPLETE))
        {
            await _mediator.Publish(new SignInFailedNotification(SignInFailedNotification.Errors.NotVerified), cancellationToken);
            return;
        }

        response.EnsureSuccessStatusCode();

        var responseBody = await response.Content.ReadFromJsonNotNullAsync<SignInPostResponseBody>(cancellationToken: cancellationToken);

        string accessToken = responseBody.AccessBearerToken;
        string refreshToken = responseBody.RefreshBearerToken;

        var setAccessTokenTask = _authenticationTokenStore.SetAccessTokenAsync(accessToken);
        var setRefreshTokenTask = _authenticationTokenStore.SetRefreshTokenAsync(refreshToken);

        await setAccessTokenTask;
        await setRefreshTokenTask;

        await _mediator.Publish(new SignInSuccessNotification(), cancellationToken);
    }
}
