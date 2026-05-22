using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Application.Services;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using Lexicom.Mvvm;
using Lexicom.Mvvm.Extensions;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;

public partial class NavigationUserViewModel : DisposableObservableObject
{
    private readonly IMessenger _messenger;
    private readonly IUserService _userService;

    public NavigationUserViewModel(
        IMessenger messenger, 
        IUserService userService)
    {
        _messenger = messenger;
        _userService = userService;
    }

    [ObservableProperty]
    public partial string? FirstName { get; set; }

    [ObservableProperty]
    public partial string? LastName { get; set; }

    [ObservableProperty]
    public partial bool IsLoggedIn { get; set; }

    public async Task LoadAsync()
    {
        await RefreshAsync();
    }

    private async Task RefreshAsync()
    {
        User? user = await _userService.GetLoggedInUserAsync();

        if (user is not null)
        {
            IsLoggedIn = true;
            FirstName = "Alex";
            LastName = "Stroot";
        }
        else
        {
            IsLoggedIn = false;
        }
    }

    [RelayCommand]
    private async Task ShowPreferencesDialogAsync()
    {
        await _messenger.SendAsync(new PreferencesDialogShowMessage());
    }

    [RelayCommand]
    private async Task ShowSignInDialogAsync()
    {
        await _messenger.SendAsync(new SignInDialogShowMessage());
    }

    [RelayCommand]
    private async Task ShowProfileDialogAsync()
    {
        await _messenger.SendAsync(new ProfileDialogShowMessage());
    }
}
