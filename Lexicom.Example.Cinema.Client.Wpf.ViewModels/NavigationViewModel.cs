using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Mvvm;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;

public partial class NavigationViewModel : DisposableObservableObject
{
    private readonly IViewModelFactory _viewModelFactory;

    public NavigationViewModel(
        NavigationDomainsViewModel domainsViewModel,
        NavigationUserViewModel userViewModel,
        IViewModelFactory viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;

        DomainsViewModel = domainsViewModel;
        UserViewModel = userViewModel;
    }

    public NavigationDomainsViewModel DomainsViewModel { get; }
    public NavigationUserViewModel UserViewModel { get; }
    public NavigationPagesViewModel<NavigationPageMovieViewModel>? MoviePagesViewModel { get; set; }
    public NavigationPagesViewModel<NavigationPageDirectorViewModel>? DirectorPagesViewModel { get; set; }
    public NavigationPagesViewModel<NavigationPageActorViewModel>? ActorPagesViewModel { get; set; }

    public override void Dispose()
    {
        DomainsViewModel.Dispose();
        UserViewModel.Dispose();
        MoviePagesViewModel?.Dispose();
        DirectorPagesViewModel?.Dispose();
        ActorPagesViewModel?.Dispose();

        base.Dispose();
    }

    public async Task LoadAsync()
    {
        MoviePagesViewModel = _viewModelFactory.Create<NavigationPagesViewModel<NavigationPageMovieViewModel>, Domain>(Domain.Movies);
        DirectorPagesViewModel = _viewModelFactory.Create<NavigationPagesViewModel<NavigationPageDirectorViewModel>, Domain>(Domain.Directors);
        ActorPagesViewModel = _viewModelFactory.Create<NavigationPagesViewModel<NavigationPageActorViewModel>, Domain>(Domain.Actors);

        await DomainsViewModel.LoadAsync();
        await UserViewModel.LoadAsync();
        await MoviePagesViewModel.LoadAsync();
        await DirectorPagesViewModel.LoadAsync();
        await ActorPagesViewModel.LoadAsync();
    }
}
