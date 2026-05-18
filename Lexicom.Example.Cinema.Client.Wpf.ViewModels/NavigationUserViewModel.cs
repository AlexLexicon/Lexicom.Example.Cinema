using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
using Lexicom.Mvvm;
using Lexicom.Mvvm.Extensions;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;

public partial class NavigationUserViewModel : DisposableObservableObject
{
    private readonly IMessenger _messenger;

    public NavigationUserViewModel(IMessenger messenger)
    {
        _messenger = messenger;
    }

    [ObservableProperty]
    public partial string? FirstName { get; set; }

    [ObservableProperty]
    public partial string? LastName { get; set; }

    [ObservableProperty]
    public partial bool IsAuthenticated { get; set; }

    public Task LoadAsync()
    {
        FirstName = "Alex";
        LastName = "Stroot";
        IsAuthenticated = false;

        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task ShowPreferencesAsync()
    {
        await _messenger.SendAsync(new ShowPreferenceViewMessage());
    }

    [RelayCommand]
    private async Task ShowSignInAsync()
    {
        await _messenger.SendAsync(new ShowSignInViewMessage());
    }

    [RelayCommand]
    private async Task ShowProfileAsync()
    {
        await _messenger.SendAsync(new ShowProfileViewMessage());
    }
}
