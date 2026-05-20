using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class NavigationPageMovieViewModel : NavigationPageViewModel
{
    public NavigationPageMovieViewModel(
        Guid id, 
        IMessenger messenger) 
        : base(
            Domains.Movies, 
            id, 
            messenger)
    {
    }

    [ObservableProperty]
    public partial string? Title { get; set; }
    [ObservableProperty]
    public partial string? ReleaseYear { get; set; }

    public override async Task LoadAsync()
    {
        MovieGetResponse movie = await _messenger.Send(new MovieGetRequest(Id));

        Title = movie.Title;
        ReleaseYear = movie.ReleasedDateTimeOffsetUtc.Year.ToString();
    }
}
