using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using Lexicom.Mvvm;
using Lexicom.Mvvm.Extensions;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class NavigationDomainViewModel : DisposableObservableObject, IAsyncRecipient<DomainSelectedMessage>, IAsyncRecipient<OpenPagesCountChangedMessage>
{
    private readonly IMessenger _messenger;

    public NavigationDomainViewModel(
        Domains domain,
        IMessenger messenger)
    {
        _messenger = messenger;

        Domain = domain;
    }

    [ObservableProperty]
    public partial Domains Domain { get; set; }
    [ObservableProperty]
    public partial bool IsSelected { get; set; }
    [ObservableProperty]
    public partial bool IsHover { get; set; }
    [ObservableProperty]
    public partial int OpenPageCount { get; set; }

    public Task ReceiveAsync(DomainSelectedMessage message, CancellationToken cancellationToken)
    {
        IsSelected = message.SelectedDomain == Domain;

        return Task.CompletedTask;
    }

    public Task ReceiveAsync(OpenPagesCountChangedMessage message, CancellationToken cancellationToken)
    {
        if (message.Domain == Domain)
        {
            OpenPageCount = message.Count;
        }

        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task LoadedAsync()
    {
        await SelectAsync();
    }

    [RelayCommand]
    private async Task SelectAsync()
    {
        await _messenger.SendAsync(new HidePagesMessage());
        await _messenger.SendAsync(new DomainSelectedMessage(Domain));
        await _messenger.ScheduleAsync(new OpenPageMessage(Domain, Guid.Empty));
    }

    [RelayCommand]
    private void HoverEnter()
    {
        IsHover = true;
    }

    [RelayCommand]
    private void HoverLeave()
    {
        IsHover = false;
    }
}
