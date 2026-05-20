using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using Lexicom.Extensions.TimeSpans;
using Lexicom.Mvvm;
using Lexicom.Mvvm.Extensions;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class SearchMovieResultViewModel : DisposableObservableObject
{
    private readonly IMessenger _messenger;

    public SearchMovieResultViewModel(
        MovieSearchResponseNotificationMovie movie,
        IMessenger messenger)
    {
        _messenger = messenger;

        MovieId = movie.Id;
        Title = movie.Title;
        ReleaseYear = movie.ReleasedDateTimeOffsetUtc.ToString("yyyy");
        Duration = movie.Duration.ToShortestString();
        Synopsis = movie.Synopsis;
    }

    [ObservableProperty]
    public partial Guid ResultId { get; set; }
    [ObservableProperty]
    public partial Guid MovieId { get; set; }
    [ObservableProperty]
    public partial string? Title { get; set; }
    [ObservableProperty]
    public partial string? ReleaseYear { get; set; }
    [ObservableProperty]
    public partial string? Duration { get; set; }
    [ObservableProperty]
    public partial string? Synopsis { get; set; }

    [RelayCommand]
    private async Task SelectedAsync()
    {
        await _messenger.SendAsync(new HidePagesMessage());
        await _messenger.ScheduleAsync(new OpenPageMessage(Domains.Movies, MovieId));
    }
}
