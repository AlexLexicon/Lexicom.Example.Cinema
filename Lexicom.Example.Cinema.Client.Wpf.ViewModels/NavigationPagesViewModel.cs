using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
using Lexicom.Mvvm;
using Lexicom.Mvvm.Extensions;
using Lexicom.Wpf.Amenities.Threading;
using System.Collections.ObjectModel;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;

public partial class NavigationPagesViewModel<TNavigationPageViewModel> : DisposableObservableObject, IAsyncRecipient<DomainSelectedNotification>, IAsyncRecipient<OpenPageMessage>, IAsyncRecipient<ClosePageMessage> where TNavigationPageViewModel : NavigationPageViewModel
{
    private readonly IMessenger _messenger;
    private readonly IViewModelFactory _viewModelFactory;
    private readonly IDispatcher _dspatcher;

    public NavigationPagesViewModel(
        IMessenger messenger,
        IViewModelFactory viewModelFactory,
        IDispatcher dspatcher)
    {
        _messenger = messenger;
        _viewModelFactory = viewModelFactory;
        _dspatcher = dspatcher;

        Type pageViewModelType = typeof(TNavigationPageViewModel);
        if (pageViewModelType == typeof(NavigationPageMovieViewModel))
        {
            Domain = Domains.Movies;
        }
        else if (pageViewModelType == typeof(NavigationPageDirectorViewModel))
        {
            Domain = Domains.Directors;
        }
        else if (pageViewModelType == typeof(NavigationPageActorViewModel))
        {
            Domain = Domains.Actors;
        }
        else
        {
            throw new NotSupportedException($"The type '{pageViewModelType.FullName}' is not a valid type because it was not able to be converted to a '{typeof(Domains).FullName}'.");
        }

        PageViewModels = [];
    }

    public ObservableCollection<TNavigationPageViewModel> PageViewModels { get; set; }

    public Domains Domain { get; }

    [ObservableProperty]
    public partial NavigationPageSearchViewModel? PageSearchViewModel { get; set; }

    [ObservableProperty]
    public partial bool IsVisible { get; set; }

    [ObservableProperty]
    public partial bool HasPageViewModels { get; set; }

    public async Task LoadAsync()
    {
        PageSearchViewModel = _viewModelFactory.Create<NavigationPageSearchViewModel, Domains>(Domain);

        await _messenger.SendAsync(new HidePagesNotification());
        await _messenger.SendAsync(new OpenPageMessage(Domain, Guid.Empty));
    }

    public Task ReceiveAsync(DomainSelectedNotification message, CancellationToken cancellationToken)
    {
        IsVisible = message.SelectedDomain == Domain;

        return Task.CompletedTask;
    }

    public async Task ReceiveAsync(OpenPageMessage message, CancellationToken cancellationToken)
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

            await _messenger.SendAsync(new OpenPagesCountChangedMessage(Domain, PageViewModels.Count), cancellationToken);
        }
    }

    public async Task ReceiveAsync(ClosePageMessage message, CancellationToken cancellationToken)
    {
        if (message.Domain == Domain)
        {
            TNavigationPageViewModel? pageViewModel = PageViewModels.FirstOrDefault(pvm => pvm.Id == message.PageId);

            if (pageViewModel is not null)
            {
                PageViewModels.Remove(pageViewModel);

                HasPageViewModels = PageViewModels.Any();
            }

            await _messenger.SendAsync(new OpenPagesCountChangedMessage(Domain, PageViewModels.Count), cancellationToken);
        }
    }

    [RelayCommand]
    private async Task DismissAsync()
    {
        await _messenger.SendAsync(new HidePagesNotification());
        await _messenger.SendAsync(new DismissPageNotification(Domain));
        await _messenger.SendAsync(new OpenPageMessage(Domain, Guid.Empty));
    }

    [RelayCommand]
    private async Task CreatePageAsync()
    {
        await _messenger.SendAsync(new CreatePageNotification(Domain));
    }
}
