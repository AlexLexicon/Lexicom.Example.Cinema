using Lexicom.Mvvm;
using Lexicom.Mvvm.Support;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Services;

public interface INavigationMoviePageService
{
    Task<int> GetPagesCountAsync();
}
public class NavigationMoviePageService : INavigationMoviePageService
{
    private readonly ILogger<NavigationMoviePageService> _logger;
    private readonly IWeakViewModelReferenceCollection<NavigationPagesViewModel<NavigationPageMovieViewModel>> _viewModels;

    public NavigationMoviePageService(
        ILogger<NavigationMoviePageService> logger,
        IWeakViewModelReferenceCollection<NavigationPagesViewModel<NavigationPageMovieViewModel>> viewModels)
    {
        _logger = logger;
        _viewModels = viewModels;
    }

    public Task<int> GetPagesCountAsync()
    {
        var viewModels = _viewModels.ToList();

        if (viewModels.Count is > 1)
        {
            _logger.LogCritical($"There was more than one '{nameof(NavigationPagesViewModel<>)}<{nameof(NavigationPageMovieViewModel)}>' view model.");
        }

        var vm = viewModels.FirstOrDefault();

        int count = 0;
        if (vm is not null)
        {
            count = vm.PageViewModels.Count;
        }

        return Task.FromResult(count);
    }
}
