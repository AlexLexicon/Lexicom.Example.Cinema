using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class NavigationPageDirectorViewModel : NavigationPageViewModel
{
    public NavigationPageDirectorViewModel(
        Guid id, 
        IMessenger messenger) 
        : base(
            Domains.Directors, 
            id, 
            messenger)
    {
    }

    [ObservableProperty]
    public partial string? Name { get; set; }

    public override async Task LoadAsync()
    {
        DirectorGetResponse movie = await _messenger.Send(new DirectorGetRequest(Id));

        Name = movie.Name;
    }
}
