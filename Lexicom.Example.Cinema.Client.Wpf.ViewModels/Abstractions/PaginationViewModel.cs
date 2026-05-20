using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using Lexicom.Mvvm;
using System.Collections.ObjectModel;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
public abstract partial class PaginationViewModel : DisposableObservableObject, IAsyncRecipient<SearchInitiateMessage>
{
    private const int TOTAL_PAGE_NUMBER_BUTTONS_PER_SIDE = 2;

    protected readonly IMessenger _messenger;

    public PaginationViewModel(
        Domains domain,
        IMessenger messenger)
    {
        _messenger = messenger;

        PageLimit = 25;
        MinimumPageIndex = 0;
        CurrentPageIndex = 0;
        MaximumPageIndex = 1;

        Domain = domain;
        PreviousPageNumbers = [];
        CurrentPageNumber = 1;
        NextPageNumbers = [];
    }

    protected int PageLimit { get; private set; }
    private int MinimumPageIndex { get; set; }
    protected int CurrentPageIndex
    {
        get;
        private set => field = Math.Clamp(value, MinimumPageIndex, MaximumPageIndex);
    }
    private int MaximumPageIndex { get; set; }

    [ObservableProperty]
    public partial Domains Domain { get; set; }
    [ObservableProperty]
    public partial ObservableCollection<int> PreviousPageNumbers { get; set; }
    [ObservableProperty]
    public partial int CurrentPageNumber { get; set; }
    [ObservableProperty]
    public partial ObservableCollection<int> NextPageNumbers { get; set; }
    [ObservableProperty]
    public partial bool IsFirstPage { get; set; }
    [ObservableProperty]
    public partial int FirstPageNumber { get; set; }
    [ObservableProperty]
    public partial bool IsLastPage { get; set; }
    [ObservableProperty]
    public partial int LastPageNumber { get; set; }

    public async Task ReceiveAsync(SearchInitiateMessage message, CancellationToken cancellationToken)
    {
        if (message.Domain == Domain)
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
        await SearchWhenCurrentPageIndexDifferentAsync(CurrentPageIndex + 1);
    }

    [RelayCommand]
    private async Task PreviousPageAsync()
    {
        await SearchWhenCurrentPageIndexDifferentAsync(CurrentPageIndex - 1);
    }

    [RelayCommand]
    private async Task FirstPageAsync()
    {
        await SearchWhenCurrentPageIndexDifferentAsync(MinimumPageIndex);
    }

    [RelayCommand]
    private async Task LastPageAsync()
    {
        await SearchWhenCurrentPageIndexDifferentAsync(MaximumPageIndex);
    }

    [RelayCommand]
    private async Task PageAsync(int pageNumber)
    {
        await SearchWhenCurrentPageIndexDifferentAsync(pageNumber - 1);
    }

    private async Task SearchWhenCurrentPageIndexDifferentAsync(int newCurrentPageIndex)
    {
        int previousPageIndex = CurrentPageIndex;
        CurrentPageIndex = newCurrentPageIndex;

        if (previousPageIndex != CurrentPageIndex)
        {
            await SearchAsync();
        }
    }
}
