using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using Lexicom.Mvvm;
using Lexicom.Mvvm.Extensions;
using System.Collections.ObjectModel;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;

public partial class NavigationPagesViewModel<TNavigationPageViewModel> : DisposableObservableObject, IAsyncRecipient<NavigationDomainSelectMessage>, IAsyncRecipient<PageOpenMessage>, IAsyncRecipient<PageCloseMessage> where TNavigationPageViewModel : AbstractNavigationPageViewModel
{
    private readonly IMessenger _messenger;
    private readonly IViewModelFactory _viewModelFactory;

    public NavigationPagesViewModel(
        Domain domain,
        IMessenger messenger,
        IViewModelFactory viewModelFactory)
    {
        _messenger = messenger;
        _viewModelFactory = viewModelFactory;

        Domain = domain;

        PageViewModels = [];
    }

    public ObservableCollection<TNavigationPageViewModel> PageViewModels { get; set; }

    public Domain Domain { get; }

    [ObservableProperty]
    public partial NavigationPageSearchViewModel? PageSearchViewModel { get; set; }

    [ObservableProperty]
    public partial bool IsSelected { get; set; }

    [ObservableProperty]
    public partial bool HasPageViewModels { get; set; }

    public override void Dispose()
    {
        PageSearchViewModel?.Dispose();
        PageViewModels.DisposeChildren();

        base.Dispose();
    }

    public Task LoadAsync()
    {
        PageSearchViewModel = _viewModelFactory.Create<NavigationPageSearchViewModel, Domain>(Domain);

        return Task.CompletedTask;
        //await _messenger.SendAsync(new HidePagesMessage());
        //await _messenger.SendAsync(new OpenPageMessage(Domain, Guid.Empty));
    }

    public Task ReceiveAsync(NavigationDomainSelectMessage message, CancellationToken cancellationToken)
    {
        IsSelected = message.Domain == Domain;

        return Task.CompletedTask;
    }

    public async Task ReceiveAsync(PageOpenMessage message, CancellationToken cancellationToken)
    {
        if (message.Domain == Domain && message.PageId != Guid.Empty)
        {
            bool pageViewModelExists = PageViewModels.Any(pvm => pvm.Id == message.PageId);

            if (!pageViewModelExists)
            {
                var vm = _viewModelFactory.Create<TNavigationPageViewModel, Guid>(message.PageId);

                await vm.LoadAsync();

                PageViewModels.Add(vm);

                HasPageViewModels = PageViewModels.Any();
            }
        }
    }

    public async Task ReceiveAsync(PageCloseMessage message, CancellationToken cancellationToken)
    {
        if (message.Domain == Domain && message.PageId != Guid.Empty)
        {
            TNavigationPageViewModel? pageViewModel = PageViewModels.FirstOrDefault(pvm => pvm.Id == message.PageId);

            if (pageViewModel is not null)
            {
                PageViewModels.Remove(pageViewModel);

                pageViewModel.Dispose();

                HasPageViewModels = PageViewModels.Any();
            }
        }
    }

    [RelayCommand]
    private async Task DismissPagesAsync()
    {
        //await _messenger.SendAsync(new HidePagesMessage());
        await _messenger.SendAsync(new DismissPagesMessage(Domain));
        //await _messenger.SendAsync(new OpenPageMessage(Domain, Guid.Empty));
    }

    [RelayCommand]
    private async Task CreatePageAsync()
    {
        await _messenger.SendAsync(new CreatePageMessage(Domain));
    }
}
