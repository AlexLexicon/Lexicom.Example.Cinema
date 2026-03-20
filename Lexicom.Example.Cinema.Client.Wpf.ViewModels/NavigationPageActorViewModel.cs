using CommunityToolkit.Mvvm.ComponentModel;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class NavigationPageActorViewModel : NavigationPageViewModel
{
    public NavigationPageActorViewModel(Guid id, IMediator mediator) : base(Domains.Actors, id, mediator)
    {
    }

    [ObservableProperty]
    private string? _name;

    public override async Task LoadAsync()
    {
        ActorGetResponse movie = await _mediator.Send(new ActorGetRequest(Id));

        Name = movie.Name;
    }
}
