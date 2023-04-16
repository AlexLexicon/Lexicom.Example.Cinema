using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Concentrate.Wpf.Themes;
using Lexicom.Mvvm;
using System.Windows.Input;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public sealed partial class MainWindowViewModel : ObservableObject, IShowableViewModel
{
    private readonly IThemeService _themeService;

    public MainWindowViewModel(
        IThemeService themeService,
        PreferencesViewModel preferencesViewModel,
        NavigationViewModel navigationViewModel,
        SearchMovieViewModel? searchMovieViewModel,
        PageMovieViewModel pageMovieViewModel,
        PageMovieFormViewModel? pageMovieFormViewModel,
        SignInViewModel? signInViewModel,
        PopupViewModel? popupViewModel)
    {
        _themeService = themeService;

        PreferencesViewModel = preferencesViewModel;
        NavigationViewModel = navigationViewModel;
        SearchMovieViewModel = searchMovieViewModel;
        PageMovieViewModel = pageMovieViewModel;
        PageMovieFormViewModel = pageMovieFormViewModel;
        SignInViewModel = signInViewModel;
        PopupViewModel = popupViewModel;
    }

    public ICommand? ShowCommand { private get; set; }

    [ObservableProperty]
    private PreferencesViewModel? _preferencesViewModel;
    [ObservableProperty]
    private NavigationViewModel? _navigationViewModel;
    [ObservableProperty]
    private SearchMovieViewModel? _searchMovieViewModel;
    [ObservableProperty]
    private PageMovieViewModel? _pageMovieViewModel;
    [ObservableProperty]
    private PageMovieFormViewModel? _pageMovieFormViewModel;
    [ObservableProperty]
    private SignInViewModel? _signInViewModel;
    [ObservableProperty]
    private PopupViewModel? _popupViewModel;

    [RelayCommand]
    private async Task LoadedAsync()
    {
        await _themeService.LoadThemeAsync();

        ShowCommand?.Execute(null);
    }
}
