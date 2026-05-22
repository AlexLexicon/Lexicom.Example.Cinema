using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using Lexicom.Mvvm.Extensions;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class PageMovieViewModel : PageViewModel
{
    private readonly IMessenger _messenger;

    public PageMovieViewModel(IMessenger messenger) : base(Domain.Movies)
    {
        _messenger = messenger;
    }

    [ObservableProperty]
    public partial string? Title { get; set; }
    [ObservableProperty]
    public partial string? ReleaseDateTime { get; set; }
    [ObservableProperty]
    public partial bool HasHours { get; set; }
    [ObservableProperty]
    public partial int Hours { get; set; }
    [ObservableProperty]
    public partial int Minutes { get; set; }
    [ObservableProperty]
    public partial string? Synopsis { get; set; }
    [ObservableProperty]
    public partial string? Rating { get; set; }
    [ObservableProperty]
    public partial string? ReviewsTotal { get; set; }

    public override async Task OpenedAsync()
    {
        MovieGetResponse movie = await _messenger.Send(new MovieGetRequest(Id));

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
        await _messenger.SendAsync(new AddDirectorToMovieMessage());
    }

    [RelayCommand]
    private async Task AddActorAsync()
    {
        await _messenger.SendAsync(new AddActorToMovieMessage());
    }

    [RelayCommand]
    private async Task CreateReviewAsync()
    {
        await _messenger.SendAsync(new CreateReviewMessage());
    }

    [RelayCommand]
    private async Task EditAsync()
    {
        await _messenger.SendAsync(new EditMovieMessage());
    }
}
