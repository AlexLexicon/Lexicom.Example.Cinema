using Lexicom.Mvvm;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;

public partial class NavigationViewModel : DisposableObservableObject
{
    public NavigationViewModel(
        NavigationDomainsViewModel domainChoiceViewModel,
        NavigationUserViewModel userSectionViewModel,
        NavigationPagesViewModel<NavigationPageMovieViewModel> pagesMoviesViewModel,
        NavigationPagesViewModel<NavigationPageDirectorViewModel> pagesDirectorViewModel,
        NavigationPagesViewModel<NavigationPageActorViewModel> pagesActorViewModel)
    {

        DomainChoiceViewModel = domainChoiceViewModel;
        UserSectionViewModel = userSectionViewModel;
        PagesMovieViewModel = pagesMoviesViewModel;
        PagesDirectorViewModel = pagesDirectorViewModel;
        PagesActorViewModel = pagesActorViewModel;
    }

    public NavigationDomainsViewModel DomainChoiceViewModel { get; }
    public NavigationUserViewModel UserSectionViewModel { get; }
    public NavigationPagesViewModel<NavigationPageMovieViewModel> PagesMovieViewModel { get; }
    public NavigationPagesViewModel<NavigationPageDirectorViewModel> PagesDirectorViewModel { get; }
    public NavigationPagesViewModel<NavigationPageActorViewModel> PagesActorViewModel { get; }

    public override void Dispose()
    {
        DomainChoiceViewModel?.Dispose();
        UserSectionViewModel?.Dispose();
        PagesMovieViewModel?.Dispose();
        PagesDirectorViewModel?.Dispose();
        PagesActorViewModel?.Dispose();

        base.Dispose();
    }

    public async Task LoadAsync()
    {
        await DomainChoiceViewModel.LoadAsync();
        await UserSectionViewModel.LoadAsync();
    }
}
