using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using Lexicom.Mvvm;
using Lexicom.Mvvm.Extensions;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
public abstract partial class AbstractNavigationPageViewModel : DisposableObservableObject, IAsyncRecipient<OpenPageMessage>, IAsyncRecipient<ClosePageMessage>, IAsyncRecipient<DismissPagesMessage>
{
    protected readonly IMessenger _messenger;

    public AbstractNavigationPageViewModel(
        Domain domain,
        Guid id,
        IMessenger messenger)
    {
        _messenger = messenger;

        Domain = domain;
        Id = id;
    }

    public Domain Domain { get; }

    [ObservableProperty]
    public partial Guid Id { get; set; }
    [ObservableProperty]
    public partial bool IsSelected { get; set; }
    [ObservableProperty]
    public partial bool IsLoading { get; set; }

    public virtual Task ReceiveAsync(OpenPageMessage message, CancellationToken cancellationToken)
    {
        if (message.Domain == Domain)
        {
            IsSelected = message.PageId == Id;
        }

        return Task.CompletedTask;
    }

    public virtual Task ReceiveAsync(ClosePageMessage message, CancellationToken cancellationToken)
    {
        if (message.Domain == Domain && message.PageId == Id)
        {
            IsSelected = false;
        }

        return Task.CompletedTask;
    }

    public async Task ReceiveAsync(DismissPagesMessage message, CancellationToken cancellationToken)
    {
        if (message.Domain == Domain)
        {
            await CloseAsync();
        }
    }

    public abstract Task LoadAsync();

    [RelayCommand]
    private async Task LoadedAsync()
    {
        //when we create a new navigation page it should be selected
        IsSelected = true;
        IsLoading = true;
        await LoadAsync();
        IsLoading = false;
    }

    [RelayCommand]
    protected virtual async Task SelectAsync()
    {
        //await _messenger.SendAsync(new HidePagesMessage());
        //await _messenger.ScheduleAsync(new OpenPageMessage(Domain, Id));
    }

    [RelayCommand]
    protected virtual async Task CloseAsync()
    {
       // await _messenger.SendAsync(new HidePagesMessage());
        //await _messenger.SendAsync(new ClosePageMessage(Domain, Id));
        //await _messenger.ScheduleAsync(new OpenPageMessage(Domain, Guid.Empty));
    }
}
