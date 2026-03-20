using CommunityToolkit.Mvvm.ComponentModel;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class NavigationPageMovieViewModel : NavigationPageViewModel
{
    public NavigationPageMovieViewModel(Guid id, IMediator mediator) : base(Domains.Movies, id, mediator)
    {
    }

    [ObservableProperty]
    private string? _title;
    [ObservableProperty]
    private string? _releaseYear;

    public override async Task LoadAsync()
    {
        MovieGetResponse movie = await _mediator.Send(new MovieGetRequest(Id));

        Title = movie.Title;
        ReleaseYear = movie.ReleasedDateTimeOffsetUtc.Year.ToString();
    }
}
