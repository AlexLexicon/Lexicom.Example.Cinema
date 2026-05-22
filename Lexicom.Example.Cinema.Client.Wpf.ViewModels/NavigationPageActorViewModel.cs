using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
using Lexicom.Mvvm.Extensions;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class NavigationPageActorViewModel : AbstractNavigationPageViewModel
{
    public NavigationPageActorViewModel(
        Guid id, 
        IMessenger messenger) 
        : base(
            Domain.Actors, 
            id, 
            messenger)
    {
    }

    [ObservableProperty]
    public partial string? Name { get; set; }

    public override async Task LoadAsync()
    {
        ActorGetResponse movie = await _messenger.SendAsync(new ActorGetRequest(Id));

        Name = movie.Name;
    }
}
