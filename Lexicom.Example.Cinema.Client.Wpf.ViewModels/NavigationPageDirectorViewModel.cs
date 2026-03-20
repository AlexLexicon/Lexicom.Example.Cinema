using CommunityToolkit.Mvvm.ComponentModel;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class NavigationPageDirectorViewModel : NavigationPageViewModel
{
    public NavigationPageDirectorViewModel(Guid id, IMediator mediator) : base(Domains.Directors, id, mediator)
    {
    }

    [ObservableProperty]
    private string? _name;

    public override async Task LoadAsync()
    {
        DirectorGetResponse movie = await _mediator.Send(new DirectorGetRequest(Id));

        Name = movie.Name;
    }
}
