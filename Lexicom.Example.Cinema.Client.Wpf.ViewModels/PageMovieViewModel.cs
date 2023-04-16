using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class PageMovieViewModel : PageViewModel
{
    private readonly IMediator _mediator;

    public PageMovieViewModel(IMediator mediator) : base(Domains.Movies)
    {
        _mediator = mediator;
    }

    [ObservableProperty]
    private string? _title;
    [ObservableProperty]
    private string? _releaseDateTime;
    [ObservableProperty]
    private bool _hasHours;
    [ObservableProperty]
    private int _hours;
    [ObservableProperty]
    private int _minutes;
    [ObservableProperty]
    private string? _synopsis;
    [ObservableProperty]
    private string? _rating;
    [ObservableProperty]
    private string? _reviewsTotal;

    public override async Task OpenedAsync()
    {
        MovieGetResponse movie = await _mediator.Send(new MovieGetRequest(Id));

        double rating = 0.5;

        Title = movie.Title;
        ReleaseDateTime = movie.ReleasedDateTimeOffsetUtc.ToString("MMMM d yyyy");
        HasHours = movie.Duration.Hours is > 0;
        Hours = movie.Duration.Hours;
        Minutes = movie.Duration.Minutes;
        Synopsis = movie.Synopsis;
        Rating = rating.ToString("0.#");
        ReviewsTotal = "1k";
    }

    [RelayCommand]
    private async Task AddDirectorAsync()
    {
        await _mediator.Publish(new AddDirectorToMovieNotification());
    }

    [RelayCommand]
    private async Task AddActorAsync()
    {
        await _mediator.Publish(new AddActorToMovieNotification());
    }

    [RelayCommand]
    private async Task CreateReviewAsync()
    {
        await _mediator.Publish(new CreateReviewNotification());
    }

    [RelayCommand]
    private async Task EditAsync()
    {
        await _mediator.Publish(new EditMovieNotification());
    }
}
