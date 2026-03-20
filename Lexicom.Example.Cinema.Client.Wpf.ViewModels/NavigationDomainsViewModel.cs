using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Mvvm;
using System.Collections.ObjectModel;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class NavigationDomainsViewModel : ObservableObject
{
    private readonly IViewModelFactory _viewModelFactory;

    public NavigationDomainsViewModel(IViewModelFactory viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;

        _domainViewModels = new ObservableCollection<NavigationDomainViewModel>();
    }

    [ObservableProperty]
    private ObservableCollection<NavigationDomainViewModel> _domainViewModels;

    [RelayCommand]
    private void Loaded()
    {
        //we reverse the DomainChoices so that the first one is created (and selected) last
        IEnumerable<Domains> reversedDomains = Enum
            .GetValues<Domains>()
            .Reverse();

        foreach (Domains domain in reversedDomains)
        {
            var domainViewModel = _viewModelFactory.Create<NavigationDomainViewModel, Domains>(domain);

            //we insert at the top so that the last item (which will be selected by default) will show up first
            DomainViewModels.Insert(0, domainViewModel);
        }
    }
}
