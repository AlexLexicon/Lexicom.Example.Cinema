using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class NavigationPageSearchViewModel : AbstractNavigationPageViewModel
{
    public NavigationPageSearchViewModel(
        Domain domain, 
        IMessenger messenger) 
        : base(
            domain, 
            Guid.Empty, 
            messenger)
    {
    }

    public override Task LoadAsync()
    {
        return Task.CompletedTask;
    }
}
