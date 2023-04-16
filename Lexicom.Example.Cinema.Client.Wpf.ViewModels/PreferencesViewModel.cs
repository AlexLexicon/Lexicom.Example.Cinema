using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Concentrate.Wpf.Themes;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class PreferencesViewModel : ObservableObject, INotificationHandler<ShowPreferenceViewNotification>
{
    private readonly IThemeService _themeService;

    public PreferencesViewModel(IThemeService themeService)
    {
        _themeService = themeService;
    }

    [ObservableProperty]
    private bool _isVisible;
    [ObservableProperty]
    private IReadOnlyList<string>? _themes;
    [ObservableProperty]
    private string? _selectedTheme;

    public Task Handle(ShowPreferenceViewNotification notification, CancellationToken cancellationToken)
    {
        IsVisible = true;

        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task LoadedAsync()
    {
        var getThemesTask = _themeService.GetThemesAsync();
        var getThemeTask = _themeService.GetThemeAsync();

        Themes = await getThemesTask;
        SelectedTheme = await getThemeTask;
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
}
