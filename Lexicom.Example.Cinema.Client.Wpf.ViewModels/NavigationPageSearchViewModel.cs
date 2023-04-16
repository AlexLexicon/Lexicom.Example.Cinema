using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class NavigationPageSearchViewModel : NavigationPageViewModel
{
    public NavigationPageSearchViewModel(Domains domain, IMediator mediator) : base(domain, Guid.Empty, mediator)
    {
    }

    public override Task LoadAsync()
    {
        return Task.CompletedTask;
    }
}
