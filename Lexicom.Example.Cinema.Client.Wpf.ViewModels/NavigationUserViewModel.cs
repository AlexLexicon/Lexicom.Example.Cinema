using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class NavigationUserViewModel : ObservableObject
{
    private readonly IMediator _mediator;

    public NavigationUserViewModel(IMediator mediator)
    {
        _mediator = mediator;

        FirstName = "Alex";
        LastName = "Stroot";
    }

    [ObservableProperty]
    private string? _firstName;
    [ObservableProperty]
    private string? _lastName;
    [ObservableProperty]
    private bool _isAuthenticated;

    [RelayCommand]
    private async Task ShowPreferencesAsync()
    {
        await _mediator.Publish(new ShowPreferenceViewNotification());
    }

    [RelayCommand]
    private async Task ShowSignInAsync()
    {
        await _mediator.Publish(new ShowSignInViewNotification());
    }

    [RelayCommand]
    private async Task ShowProfileAsync()
    {
        await _mediator.Publish(new ShowProfileViewNotification());   
    }
}
