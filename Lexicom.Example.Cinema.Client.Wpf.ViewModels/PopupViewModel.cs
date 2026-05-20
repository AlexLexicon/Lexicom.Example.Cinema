using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using Lexicom.Mvvm;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class PopupViewModel : DisposableObservableObject, IAsyncRecipient<FeatureRequiresSignInMessage>
{
    public PopupViewModel()
    {
    }

    [ObservableProperty]
    public partial bool IsVisible { get; set; }
    [ObservableProperty]
    public partial string? Title { get; set; }
    [ObservableProperty]
    public partial string? Message { get; set; }

    public Task ReceiveAsync(FeatureRequiresSignInMessage message, CancellationToken cancellationToken)
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
