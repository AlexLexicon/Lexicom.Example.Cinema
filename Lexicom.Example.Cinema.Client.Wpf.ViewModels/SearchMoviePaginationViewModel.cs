using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using Lexicom.Mvvm;
using Lexicom.Mvvm.Extensions;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class SearchMoviePaginationViewModel : PaginationViewModel, IAsyncRecipient<MovieSearchResponseNotification>
{
    public SearchMoviePaginationViewModel(IMessenger messenger) 
        : base(
            Domain.Movies, 
            messenger)
    {
    }

    protected override async Task SearchAsync()
    {
        await _messenger.SendAsync(new SearchStartedMessage());
        await _messenger.SendAsync(new MovieSearchRequestNotification(CurrentPageIndex, PageLimit));
    }

    public Task ReceiveAsync(MovieSearchResponseNotification message, CancellationToken cancellationToken)
    {
        Update(message.TotalCount);

        return Task.CompletedTask;
    }
}
