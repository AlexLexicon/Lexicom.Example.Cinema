using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public abstract partial class SearchViewModel : ObservableObject, INotificationHandler<OpenPageMessage>, INotificationHandler<HidePagesMessage>, INotificationHandler<SearchStartedMessage>
{
    private readonly IMediator _mediator;

    protected SearchViewModel(
        Domains domain,
        IMediator mediator)
    {
        _mediator = mediator;

        Domain = domain;

        SearchTextChanged();
    }

    [ObservableProperty]
    private Domains _domain;
    [ObservableProperty]
    private bool _isVisible;
    [ObservableProperty]
    private string? _searchText;
    [ObservableProperty]
    private bool _isHintVisible;
    [ObservableProperty]
    private bool _isSearchResultsVisible;

    public bool IsEmptySearch
    {
        get;
        set
        {
            SetProperty(ref field, value);
            IsSearchResultsVisible = !IsEmptySearch && !IsSearching;
        }
    }

    public bool IsSearching
    {
        get;
        set
        {
            SetProperty(ref field, value);
            IsSearchResultsVisible = !IsEmptySearch && !IsSearching;
        }
    }

    public async Task Handle(OpenPageMessage notification, CancellationToken cancellationToken)
    {
        IsVisible = false;

        if (notification.PageId == Guid.Empty && notification.Domain == Domain)
        {
            IsVisible = true;

            await SearchAsync();
        }
    }

    public Task Handle(HidePagesMessage notification, CancellationToken cancellationToken)
    {
        IsVisible = false;

        return Task.CompletedTask;
    }

    public Task Handle(SearchStartedMessage notification, CancellationToken cancellationToken)
    {
        IsEmptySearch = false;
        IsSearching = true;

        return Task.CompletedTask;
    }

    [RelayCommand]
    private void SearchTextChanged()
    {
        IsHintVisible = string.IsNullOrEmpty(SearchText);
    }

    [RelayCommand]
    private async Task SearchAsync()
    {
        await _mediator.Publish(new SearchInitiateMessage(Domain));
    }
}
