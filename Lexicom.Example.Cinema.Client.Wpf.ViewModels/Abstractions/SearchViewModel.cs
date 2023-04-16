using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public abstract partial class SearchViewModel : ObservableObject, INotificationHandler<OpenPageNotification>, INotificationHandler<HidePagesNotification>, INotificationHandler<SearchStartedNotification>
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
    private bool _isEmptySearch;
    public bool IsEmptySearch
    {
        get => _isEmptySearch;
        set
        {
            SetProperty(ref _isEmptySearch, value);
            IsSearchResultsVisible = !IsEmptySearch && !IsSearching;
        }
    }
    private bool _isSearching;
    public bool IsSearching
    {
        get => _isSearching;
        set
        {
            SetProperty(ref _isSearching, value);
            IsSearchResultsVisible = !IsEmptySearch && !IsSearching;
        }
    }

    public async Task Handle(OpenPageNotification notification, CancellationToken cancellationToken)
    {
        IsVisible = false;

        if (notification.Id == Guid.Empty && notification.Domain == Domain)
        {
            IsVisible = true;

            await SearchAsync();
        }
    }

    public Task Handle(HidePagesNotification notification, CancellationToken cancellationToken)
    {
        IsVisible = false;

        return Task.CompletedTask;
    }

    public Task Handle(SearchStartedNotification notification, CancellationToken cancellationToken)
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
        await _mediator.Publish(new SearchInitiateNotification(Domain));
    }
}
