using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
using Lexicom.Mvvm;
using Lexicom.Mvvm.Extensions;
using System.Collections.ObjectModel;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class SearchMovieViewModel : SearchViewModel, IAsyncRecipient<MovieSearchResponseNotification>
{
    private readonly IViewModelFactory _viewModelFactory;

    public SearchMovieViewModel(
        IMessenger messenger,
        IViewModelFactory viewModelFactory,
        SearchMoviePaginationViewModel paginationViewModel) 
        : base(
            Domain.Movies, 
            messenger)
    {
        _viewModelFactory = viewModelFactory;

        PaginationViewModel = paginationViewModel;
        ResultViewModels = [];
        SortOn =
        [
            "Title",
            "ReleaseDate",
            "Duration",
            "Synopsis",
        ];
        SelectedSortOn = SortOn.First();

        IsTitleFilter = true;
        IsReleaseDateFilter = true;
        IsDurationFilter = true;
        IsSynopsisFilter = true;
    }

    [ObservableProperty]
    public partial SearchMoviePaginationViewModel PaginationViewModel { get; set; }
    [ObservableProperty]
    public partial ObservableCollection<SearchMovieResultViewModel> ResultViewModels { get; set; }
    [ObservableProperty]
    public partial bool IsTitleFilter { get; set; }
    [ObservableProperty]
    public partial bool IsReleaseDateFilter { get; set; }
    [ObservableProperty]
    public partial bool IsDurationFilter { get; set; }
    [ObservableProperty]
    public partial bool IsSynopsisFilter { get; set; }
    [ObservableProperty]
    public partial IReadOnlyList<string> SortOn { get; set; }
    [ObservableProperty]
    public partial string? SelectedSortOn { get; set; }

    public override void Dispose()
    {
        PaginationViewModel?.Dispose();
        ResultViewModels.DisposeChildren();

        base.Dispose();
    }

    public Task ReceiveAsync(MovieSearchResponseNotification message, CancellationToken cancellationToken)
    {
        ResultViewModels.DisposeAndClearChildren();
        foreach (MovieSearchResponseNotificationMovie movie in message.ResultsSlice)
        {
            var viewModel = _viewModelFactory.Create<SearchMovieResultViewModel, MovieSearchResponseNotificationMovie>(movie);

            ResultViewModels.Add(viewModel);
        }

        IsEmptySearch = !ResultViewModels.Any();
        IsSearching = false;

        return Task.CompletedTask;
    }
}
