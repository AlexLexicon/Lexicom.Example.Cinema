﻿using Lexicom.Concentrate.Client.Authentication;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Handlers;
public class AddActorToMovieHandler : INotificationHandler<AddActorToMovieNotification>
{
    private readonly IMediator _mediator;
    private readonly IAuthenticationTokenStore _authenticationTokenStore;

    public AddActorToMovieHandler(
        IMediator mediator,
        IAuthenticationTokenStore authenticationTokenStore)
    {
        _mediator = mediator;
        _authenticationTokenStore = authenticationTokenStore;
    }

    public async Task Handle(AddActorToMovieNotification notification, CancellationToken cancellationToken)
    {
        bool isAuthenticated = await _authenticationTokenStore.IsAuthenticatedAsync();

        if (!isAuthenticated)
        {
            await _mediator.Publish(new FeatureRequiresSignInNotification(), cancellationToken);
        }
    }
}
