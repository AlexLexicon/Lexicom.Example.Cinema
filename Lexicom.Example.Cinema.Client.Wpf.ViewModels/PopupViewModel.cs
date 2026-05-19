using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class PopupViewModel : ObservableObject, INotificationHandler<FeatureRequiresSignInMessage>
{

    public PopupViewModel()
    {

    }

    [ObservableProperty]
    private bool _isVisible;
    [ObservableProperty]
    private string? _title;
    [ObservableProperty]
    private string? _message;

    public Task Handle(FeatureRequiresSignInMessage notification, CancellationToken cancellationToken)
    {
        IsVisible = true;

        Title = "Sign in required";
        Message = "You must sign in before you can access this feature.";

        return Task.CompletedTask;
    }

    [RelayCommand]
    private void Hide()
    {
        IsVisible = false;
    }
}
