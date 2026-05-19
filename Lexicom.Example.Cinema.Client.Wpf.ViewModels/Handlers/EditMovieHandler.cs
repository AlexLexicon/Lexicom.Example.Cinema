using Lexicom.Concentrate.Client.Authentication;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Handlers;
public class EditMovieHandler : INotificationHandler<EditMovieMessage>
{
    private readonly IMediator _mediator;
    private readonly IAuthenticationTokenStore _authenticationTokenStore;

    public EditMovieHandler(
        IMediator mediator,
        IAuthenticationTokenStore authenticationTokenStore)
    {
        _mediator = mediator;
        _authenticationTokenStore = authenticationTokenStore;
    }

    public async Task Handle(EditMovieMessage notification, CancellationToken cancellationToken)
    {
        bool isAuthenticated = await _authenticationTokenStore.IsAuthenticatedAsync();

        if (!isAuthenticated)
        {
            await _mediator.Publish(new FeatureRequiresSignInMessage(), cancellationToken);
        }
    }
}
