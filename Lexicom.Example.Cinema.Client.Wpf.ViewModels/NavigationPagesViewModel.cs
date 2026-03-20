using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
using Lexicom.Mvvm;
using Lexicom.Wpf.Amenities.Threading;
using MediatR;
using System.Collections.ObjectModel;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class NavigationPagesViewModel<TNavigationPageViewModel> : ObservableObject, INotificationHandler<DomainSelectedNotification>, INotificationHandler<OpenPageNotification>, INotificationHandler<ClosePageNotification> where TNavigationPageViewModel : NavigationPageViewModel
{
    private readonly IMediator _mediator;
    private readonly IViewModelFactory _viewModelFactory;
    private readonly IDispatcher _dspatcher;

    public NavigationPagesViewModel(
        IMediator mediator,
        IViewModelFactory viewModelFactory,
        IDispatcher dspatcher)
    {
        _mediator = mediator;
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

        _pageViewModels = new ObservableCollection<TNavigationPageViewModel>();
    }

    [ObservableProperty]
    private NavigationPageSearchViewModel? _pageSearchViewModel;
    [ObservableProperty]
    private Domains _domain;
    [ObservableProperty]
    private ObservableCollection<TNavigationPageViewModel> _pageViewModels;
    [ObservableProperty]
    private bool _isVisible;
    [ObservableProperty]
    private bool _hasPageViewModels;

    public virtual Task Handle(DomainSelectedNotification notification, CancellationToken cancellationToken)
    {
        IsVisible = notification.SelectedDomain == Domain;

        return Task.CompletedTask;
    }

    public virtual async Task Handle(OpenPageNotification notification, CancellationToken cancellationToken)
    {
        await _dspatcher.InvokeAsync(async () =>
        {
            if (notification.Domain == Domain && notification.Id != Guid.Empty)
            {
                bool pageViewModelExists = PageViewModels.Any(pvm => pvm.Id == notification.Id);

                if (!pageViewModelExists)
                {
                    var viewModel = _viewModelFactory.Create<TNavigationPageViewModel, Guid>(notification.Id);

                    PageViewModels.Add(viewModel);

                    HasPageViewModels = PageViewModels.Any();
                }

                await _mediator.Publish(new OpenPagesCountChangedNotification(Domain, PageViewModels.Count), cancellationToken);
            }
        });
    }

    public virtual async Task Handle(ClosePageNotification notification, CancellationToken cancellationToken)
    {
        if (notification.Domain == Domain)
        {
            TNavigationPageViewModel? pageViewModel = PageViewModels.FirstOrDefault(pvm => pvm.Id == notification.Id);

            if (pageViewModel is not null)
            {
                PageViewModels.Remove(pageViewModel);

                HasPageViewModels = PageViewModels.Any();
            }

            await _mediator.Publish(new OpenPagesCountChangedNotification(Domain, PageViewModels.Count), cancellationToken);
        }
    }

    [RelayCommand]
    private async Task LoadedAsync()
    {
        PageSearchViewModel = _viewModelFactory.Create<NavigationPageSearchViewModel, Domains>(Domain);

        await _mediator.Publish(new HidePagesNotification());
        await _mediator.Publish(new OpenPageNotification(Domain, Guid.Empty));
    }

    [RelayCommand]
    private async Task DismissAsync()
    {
        await _mediator.Publish(new HidePagesNotification());
        await _mediator.Publish(new DismissPageNotification(Domain));
        await _mediator.Publish(new OpenPageNotification(Domain, Guid.Empty));
    }

    [RelayCommand]
    private async Task CreatePageAsync()
    {
        await _mediator.Publish(new CreatePageNotification(Domain));
    }
}
