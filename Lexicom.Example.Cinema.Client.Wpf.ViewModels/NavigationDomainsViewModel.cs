using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Application.Services;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using Lexicom.Mvvm;
using Lexicom.Mvvm.Extensions;
using System.Collections.ObjectModel;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;

public partial class NavigationDomainsViewModel : DisposableObservableObject
{
    private readonly IMessenger _messenger;
    private readonly IViewModelFactory _viewModelFactory;
    private readonly IDomainService _domainService;

    public NavigationDomainsViewModel(
        IMessenger messenger,
        IViewModelFactory viewModelFactory,
        IDomainService domainService)
    {
        _messenger = messenger;
        _viewModelFactory = viewModelFactory;
        _domainService = domainService;

        DomainViewModels = [];
    }

    public ObservableCollection<NavigationDomainViewModel> DomainViewModels { get; }

    public override void Dispose()
    {
        DomainViewModels.DisposeChildren();

        base.Dispose();
    }

    public async Task LoadAsync()
    {
        IReadOnlyList<Domain> domains = await _domainService.GetDomainsAsync();

        DomainViewModels.DisposeAndClearChildren();
        foreach (Domain domain in domains)
        {
            var vm = _viewModelFactory.Create<NavigationDomainViewModel, Domain>(domain);

            await vm.LoadAsync();

            DomainViewModels.Add(vm);
        }

        Domain firstDomain = domains.First();

        await _messenger.SendAsync(new NavigationDomainSelectMessage(firstDomain));
    }
}
