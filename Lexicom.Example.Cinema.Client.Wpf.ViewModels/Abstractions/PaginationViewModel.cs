using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
using MediatR;
using System.Collections.ObjectModel;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
public abstract partial class PaginationViewModel : ObservableObject, INotificationHandler<SearchInitiateNotification>
{
    private const int TOTAL_PAGE_NUMBER_BUTTONS_PER_SIDE = 2;

    protected readonly IMediator _mediator;

    public PaginationViewModel(
        Domains domain,
        IMediator mediator)
    {
        _mediator = mediator;

        PageLimit = 25;
        MinimumPageIndex = 0;
        CurrentPageIndex = 0;
        MaximumPageIndex = 1;

        Domain = domain;
        _previousPageNumbers = new ObservableCollection<int>();
        CurrentPageNumber = 1;
        _nextPageNumbers = new ObservableCollection<int>();
    }

    protected int PageLimit { get; private set; }
    private int MinimumPageIndex { get; set; }
    private int _currentPageIndex;
    protected int CurrentPageIndex
    {
        get => _currentPageIndex;
        private set => _currentPageIndex = Math.Clamp(value, MinimumPageIndex, MaximumPageIndex);
    }
    private int MaximumPageIndex { get; set; }

    [ObservableProperty]
    private Domains _domain;
    [ObservableProperty]
    private ObservableCollection<int> _previousPageNumbers;
    [ObservableProperty]
    private int _currentPageNumber;
    [ObservableProperty]
    private ObservableCollection<int> _nextPageNumbers;
    [ObservableProperty]
    private bool _isFirstPage;
    [ObservableProperty]
    private int _firstPageNumber;
    [ObservableProperty]
    private bool _isLastPage;
    [ObservableProperty]
    private int _lastPageNumber;

    public async Task Handle(SearchInitiateNotification notification, CancellationToken cancellationToken)
    {
        if (notification.Domain == Domain)
        {
            CurrentPageIndex = MinimumPageIndex;

            await SearchAsync();
        }
    }

    protected abstract Task SearchAsync();

    protected virtual void Update(int totalSearchResultsCount)
    {
        int totalPages = (int)Math.Ceiling(totalSearchResultsCount / (double)PageLimit);

        if (totalPages <= 0)
        {
            PreviousPageNumbers.Clear();
            NextPageNumbers.Clear();

            MinimumPageIndex = 0;
            MaximumPageIndex = 0;
            CurrentPageIndex = 0;

            CurrentPageNumber = 0;

            IsFirstPage = true;
            IsLastPage = true;

            return;
        }

        int minPageIndex = Math.Max(0, CurrentPageIndex - TOTAL_PAGE_NUMBER_BUTTONS_PER_SIDE);

        PreviousPageNumbers.Clear();
        for (int index = CurrentPageIndex - 1; index >= minPageIndex; index--)
        {
            PreviousPageNumbers.Add(index + 1);
        }

        MaximumPageIndex = totalPages - 1;

        int subMaxPageIndex = Math.Min(CurrentPageIndex + TOTAL_PAGE_NUMBER_BUTTONS_PER_SIDE, MaximumPageIndex);

        NextPageNumbers.Clear();
        for (int index = CurrentPageIndex + 1; index <= subMaxPageIndex; index++)
        {
            NextPageNumbers.Add(index + 1);
        }

        MinimumPageIndex = 0;

        FirstPageNumber = MinimumPageIndex + 1;
        CurrentPageNumber = CurrentPageIndex + 1;
        LastPageNumber = MaximumPageIndex + 1;

        IsFirstPage = CurrentPageIndex == MinimumPageIndex;
        IsLastPage = CurrentPageIndex == MaximumPageIndex;
    }

    [RelayCommand]
    private async Task NextPageAsync()
    {
        await SearchWhenCurrentPageIndexDiffrentAsync(CurrentPageIndex + 1);
    }

    [RelayCommand]
    private async Task PreviousPageAsync()
    {
        await SearchWhenCurrentPageIndexDiffrentAsync(CurrentPageIndex - 1);
    }

    [RelayCommand]
    private async Task FirstPageAsync()
    {
        await SearchWhenCurrentPageIndexDiffrentAsync(MinimumPageIndex);
    }

    [RelayCommand]
    private async Task LastPageAsync()
    {
        await SearchWhenCurrentPageIndexDiffrentAsync(MaximumPageIndex);
    }

    [RelayCommand]
    private async Task PageAsync(int pageNumber)
    {
        await SearchWhenCurrentPageIndexDiffrentAsync(pageNumber - 1);
    }

    private async Task SearchWhenCurrentPageIndexDiffrentAsync(int newCurrentPageIndex)
    {
        int previousPageIndex = CurrentPageIndex;
        CurrentPageIndex = newCurrentPageIndex;

        if (previousPageIndex != CurrentPageIndex)
        {
            await SearchAsync();
        }
    }
}
