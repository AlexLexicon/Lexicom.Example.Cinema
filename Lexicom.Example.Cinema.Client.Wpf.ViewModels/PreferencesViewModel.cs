using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Concentrate.Wpf.Themes;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
using Lexicom.Mvvm;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;

public partial class PreferencesViewModel : DisposableObservableObject, IAsyncRecipient<ShowPreferenceViewMessage>
{
    private readonly IThemeService _themeService;

    public PreferencesViewModel(IThemeService themeService)
    {
        _themeService = themeService;
    }

    [ObservableProperty]
    public partial bool IsVisible { get; set; }

    [ObservableProperty]
    public partial IReadOnlyList<string>? Themes { get; set; }

    [ObservableProperty]
    public partial string? SelectedTheme { get; set; }

    public override void Dispose()
    {
        base.Dispose();
    }

    public async Task LoadAsync()
    {
        await RefreshAsync();
    }

    public async Task ReceiveAsync(ShowPreferenceViewMessage message, CancellationToken cancellationToken)
    {
        IsVisible = true;

        await RefreshAsync();
    }

    [RelayCommand]
    private void Hide()
    {
        IsVisible = false;
    }

    [RelayCommand]
    private async Task ThemeSelectedAsync()
    {
        if (SelectedTheme is not null)
        {
            await _themeService.SetThemeAsync(SelectedTheme);
        }
    }

    private async Task RefreshAsync()
    {
        var getThemesTask = _themeService.GetThemesAsync();
        var getThemeTask = _themeService.GetThemeAsync();

        Themes = await getThemesTask;
        SelectedTheme = await getThemeTask;
    }
}
