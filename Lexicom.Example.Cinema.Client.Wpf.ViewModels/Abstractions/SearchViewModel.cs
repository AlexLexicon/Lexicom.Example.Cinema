using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using Lexicom.Mvvm;
using Lexicom.Mvvm.Extensions;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
public abstract partial class SearchViewModel : DisposableObservableObject, IAsyncRecipient<OpenPageMessage>, IAsyncRecipient<HidePagesMessage>, IAsyncRecipient<SearchStartedMessage>
{
    private readonly IMessenger _messenger;

    protected SearchViewModel(
        Domain domain,
        IMessenger messenger)
    {
        _messenger = messenger;

        Domain = domain;

        SearchTextChanged();
    }

    [ObservableProperty]
    public partial Domain Domain { get; set; }
    [ObservableProperty]
    public partial bool IsVisible { get; set; }
    [ObservableProperty]
    public partial string? SearchText { get; set; }
    [ObservableProperty]
    public partial bool IsHintVisible { get; set; }
    [ObservableProperty]
    public partial bool IsSearchResultsVisible { get; set; }

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

    public async Task ReceiveAsync(OpenPageMessage message, CancellationToken cancellationToken)
    {
        IsVisible = false;

        if (message.PageId == Guid.Empty && message.Domain == Domain)
        {
            IsVisible = true;

            await SearchAsync();
        }
    }

    public Task ReceiveAsync(HidePagesMessage message, CancellationToken cancellationToken)
    {
        IsVisible = false;

        return Task.CompletedTask;
    }

    public Task ReceiveAsync(SearchStartedMessage message, CancellationToken cancellationToken)
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
        await _messenger.SendAsync(new SearchInitiateMessage(Domain));
    }
}
