using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Services;
using Lexicom.Mvvm;
using Lexicom.Mvvm.Extensions;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class NavigationDomainViewModel : DisposableObservableObject, IAsyncRecipient<NavigationDomainSelectMessage>, IAsyncRecipient<PageOpenMessage>, IAsyncRecipient<PageCloseMessage>
{
    private readonly IMessenger _messenger;
    private readonly INavigationDomainService _navigationDomainService;

    public NavigationDomainViewModel(
        Domain domain,
        IMessenger messenger,
        INavigationDomainService navigationDomainService)
    {
        _messenger = messenger;
        _navigationDomainService = navigationDomainService;

        Domain = domain;
    }

    [ObservableProperty]
    public partial Domain Domain { get; set; }

    [ObservableProperty]
    public partial bool IsSelected { get; set; }

    [ObservableProperty]
    public partial int PagesCount { get; set; }

    //[ObservableProperty]
    //public partial bool IsHover { get; set; }

    public Task ReceiveAsync(NavigationDomainSelectMessage message, CancellationToken cancellationToken)
    {
        IsSelected = message.Domain == Domain;

        return Task.CompletedTask;
    }

    public async Task ReceiveAsync(PageOpenMessage message, CancellationToken cancellationToken)
    {
        await RefreshAsync();
    }

    public async Task ReceiveAsync(PageCloseMessage message, CancellationToken cancellationToken)
    {
        await RefreshAsync();
    }

    public async Task LoadAsync()
    {
        await RefreshAsync();
    }

    private async Task RefreshAsync()
    {
        PagesCount = await _navigationDomainService.GetPagesCountAsync(Domain);
    }

    [RelayCommand]
    private async Task SelectAsync()
    {
        //await _messenger.SendAsync(new HidePagesMessage());
        await _messenger.SendAsync(new NavigationDomainSelectMessage(Domain));
        //await _messenger.ScheduleAsync(new OpenPageMessage(Domain, Guid.Empty));
    }

    //[RelayCommand]
    //private void HoverEnter()
    //{
    //    IsHover = true;
    //}

    //[RelayCommand]
    //private void HoverLeave()
    //{
    //    IsHover = false;
    //}
}
