using CommunityToolkit.Mvvm.ComponentModel;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Mvvm;
using MediatR;
using System.Collections.ObjectModel;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class SearchMovieViewModel : SearchViewModel, IRequestHandler<MovieSearchGetTextRequest, MovieSearchGetTextResponse>, INotificationHandler<MovieSearchResponseNotification>, IRequestHandler<MovieSearchGetFiltersRequest, MovieSearchGetFiltersResponse>
{
    private readonly IViewModelFactory _viewModelFactory;

    public SearchMovieViewModel(
        IMediator mediator,
        IViewModelFactory viewModelFactory,
        SearchMoviePaginationViewModel paginationViewModel) : base(Domains.Movies, mediator)
    {
        _viewModelFactory = viewModelFactory;
        _paginationViewModel = paginationViewModel;

        _resultViewModels = new ObservableCollection<SearchMovieResultViewModel>();
        _sortOn = new List<string>
        {
            "Title",
            "ReleaseDate",
            "Duration",
            "Synopsis",
        };
        _selectedSortOn = SortOn.First();

        IsTitleFilter = true;
        IsReleaseDateFilter = true;
        IsDurationFilter = true;
        IsSynopsisFilter = true;
    }

    [ObservableProperty]
    private SearchMoviePaginationViewModel _paginationViewModel;
    [ObservableProperty]
    private ObservableCollection<SearchMovieResultViewModel> _resultViewModels;
    [ObservableProperty]
    private bool _isTitleFilter;
    [ObservableProperty]
    private bool _isReleaseDateFilter;
    [ObservableProperty]
    private bool _isDurationFilter;
    [ObservableProperty]
    private bool _isSynopsisFilter;
    [ObservableProperty]
    private IReadOnlyList<string> _sortOn;
    [ObservableProperty]
    private string? _selectedSortOn;

    public Task<MovieSearchGetTextResponse> Handle(MovieSearchGetTextRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new MovieSearchGetTextResponse(SearchText));
    }

    public Task Handle(MovieSearchResponseNotification notification, CancellationToken cancellationToken)
    {
        ResultViewModels.Clear();

        foreach (MovieSearchResponseNotificationMovie movie in notification.ResultsSlice)
        {
            var viewModel = _viewModelFactory.Create<SearchMovieResultViewModel, MovieSearchResponseNotificationMovie>(movie);

            ResultViewModels.Add(viewModel);
        }

        IsEmptySearch = !ResultViewModels.Any();
        IsSearching = false;

        return Task.CompletedTask;
    }

    public Task<MovieSearchGetFiltersResponse> Handle(MovieSearchGetFiltersRequest request, CancellationToken cancellationToken)
    {
        return Task.FromResult(new MovieSearchGetFiltersResponse(IsTitleFilter, IsReleaseDateFilter, IsDurationFilter, IsSynopsisFilter));
    }
}
