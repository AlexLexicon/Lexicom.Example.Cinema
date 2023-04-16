using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public class SearchMoviePaginationViewModel : PaginationViewModel, INotificationHandler<MovieSearchResponseNotification>
{
    public SearchMoviePaginationViewModel(IMediator mediator) : base(Domains.Movies, mediator)
    {
    }

    protected override async Task SearchAsync() 
    {
        await _mediator.Publish(new SearchStartedNotification());
        await _mediator.Publish(new MovieSearchRequestNotification(CurrentPageIndex, PageLimit));
    }

    public Task Handle(MovieSearchResponseNotification notification, CancellationToken cancellationToken)
    {
        Update(notification.TotalCount);

        return Task.CompletedTask;
    }
}
