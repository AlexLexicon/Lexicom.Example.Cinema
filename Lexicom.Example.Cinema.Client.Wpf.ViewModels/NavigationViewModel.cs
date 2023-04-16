using CommunityToolkit.Mvvm.ComponentModel;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class NavigationViewModel : ObservableObject
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

    [ObservableProperty]
    private NavigationDomainsViewModel? _domainChoiceViewModel;
    [ObservableProperty]
    private NavigationUserViewModel? _userSectionViewModel;
    [ObservableProperty]
    private NavigationPagesViewModel<NavigationPageMovieViewModel>? _pagesMovieViewModel;
    [ObservableProperty]
    private NavigationPagesViewModel<NavigationPageDirectorViewModel>? _pagesDirectorViewModel;
    [ObservableProperty]
    private NavigationPagesViewModel<NavigationPageActorViewModel>? _pagesActorViewModel;
}
