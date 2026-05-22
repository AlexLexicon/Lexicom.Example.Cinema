using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Application.Services;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class NavigationPageMovieViewModel : AbstractNavigationPageViewModel
{
    private readonly IMovieService _movieService;

    public NavigationPageMovieViewModel(
        Guid id,
        IMessenger messenger,
        IMovieService movieService)
        : base(
            Domain.Movies,
            id,
            messenger)
    {
        _movieService = movieService;
    }

    [ObservableProperty]
    public partial string? Title { get; set; }

    [ObservableProperty]
    public partial string? ReleaseYear { get; set; }

    public override async Task LoadAsync()
    {
        var movie = await _movieService.GetMovieAsync(Id);

        MovieGetResponse movie = await _messenger.Send(new MovieGetRequest(Id));

        Title = movie.Title;
        ReleaseYear = movie.ReleasedDateTimeOffsetUtc.Year.ToString();
    }
}
