using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Extensions;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class SearchMovieResultViewModel : ObservableObject
{
    private readonly IMediator _mediator;

    public SearchMovieResultViewModel(
        MovieSearchResponseNotificationMovie movie, 
        IMediator mediator)
    {
        _mediator = mediator;

        MovieId = movie.Id;
        Title = movie.Title;
        ReleaseYear = movie.ReleasedDateTimeOffsetUtc.ToString("yyyy");
        Duration = movie.Duration.ToDurationString();
        Synopsis = movie.Synopsis;
    }

    [ObservableProperty]
    private Guid _resultId;
    [ObservableProperty]
    private Guid _movieId;
    [ObservableProperty]
    private string? _title;
    [ObservableProperty]
    private string? _releaseYear;
    [ObservableProperty]
    private string? _duration;
    [ObservableProperty]
    private string? _synopsis;

    [RelayCommand]
    private async Task SelectedAsync()
    {
        await _mediator.Publish(new HidePagesNotification());
        await _mediator.Publish(new OpenPageNotification(Domains.Movies, MovieId));
    }
}
