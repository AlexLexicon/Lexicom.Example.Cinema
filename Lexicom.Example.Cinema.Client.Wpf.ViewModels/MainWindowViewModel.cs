using CommunityToolkit.Mvvm.Input;
using Lexicom.Concentrate.Wpf.Themes;
using Lexicom.Mvvm;
using System.Windows.Input;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;

public sealed partial class MainWindowViewModel : DisposableObservableObject, IShowableViewModel
{
    private readonly IThemeService _themeService;

    public MainWindowViewModel(
        IThemeService themeService,
        PreferencesDialogViewModel preferencesDialogViewModel,
        NavigationViewModel navigationViewModel,
        SearchMovieViewModel searchMovieViewModel,
        PageMovieViewModel pageMovieViewModel,
        PageMovieFormViewModel pageMovieFormViewModel,
        SignInViewModel signInViewModel,
        PopupViewModel popupViewModel)
    {
        _themeService = themeService;

        PreferencesDialogViewModel = preferencesDialogViewModel;
        NavigationViewModel = navigationViewModel;
        SearchMovieViewModel = searchMovieViewModel;
        PageMovieViewModel = pageMovieViewModel;
        PageMovieFormViewModel = pageMovieFormViewModel;
        SignInViewModel = signInViewModel;
        PopupViewModel = popupViewModel;
    }

    public ICommand? ShowCommand { private get; set; }

    public PreferencesDialogViewModel PreferencesDialogViewModel { get; }
    public NavigationViewModel NavigationViewModel { get; }
    public SearchMovieViewModel SearchMovieViewModel { get; }
    public PageMovieViewModel PageMovieViewModel { get; }
    public PageMovieFormViewModel PageMovieFormViewModel { get; }
    public SignInViewModel SignInViewModel { get; }
    public PopupViewModel PopupViewModel { get; }

    public override void Dispose()
    {
        PreferencesDialogViewModel?.Dispose();
        NavigationViewModel?.Dispose();
        SearchMovieViewModel?.Dispose();
        PageMovieViewModel?.Dispose();
        PageMovieFormViewModel?.Dispose();
        SignInViewModel?.Dispose();
        PopupViewModel?.Dispose();

        base.Dispose();
    }

    [RelayCommand]
    private async Task LoadedAsync()
    {
        await PreferencesDialogViewModel.LoadAsync();
        await NavigationViewModel.LoadAsync();

        await _themeService.LoadThemeAsync();

        ShowCommand?.Execute(null);
    }
}
