using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using Lexicom.Mvvm.Extensions;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class NavigationDomainViewModel : ObservableObject, INotificationHandler<DomainSelectedMessage>, INotificationHandler<OpenPagesCountChangedMessage>
{
    private readonly IMessenger _mediator;

    public NavigationDomainViewModel(
        Domains domain,
        IMediator mediator)
    {
        _mediator = mediator;

        Domain = domain;
    }

    [ObservableProperty]
    private Domains _domain;
    [ObservableProperty]
    private bool _isSelected;
    [ObservableProperty]
    private bool _isHover;
    [ObservableProperty]
    private int _openPageCount;

    public Task Handle(DomainSelectedMessage notification, CancellationToken cancellationToken)
    {
        IsSelected = notification.SelectedDomain == Domain;

        return Task.CompletedTask;
    }

    public Task Handle(OpenPagesCountChangedMessage notification, CancellationToken cancellationToken)
    {
        if (notification.Domain == Domain)
        {
            OpenPageCount = notification.Count;
        }

        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task LoadedAsync()
    {
        await SelectAsync();
    }

    [RelayCommand]
    private async Task SelectAsync()
    {
        await _mediator.Publish(new HidePagesMessage());
        await _mediator.Publish(new DomainSelectedMessage(Domain));
        await _mediator.ScheduleAsync(new OpenPageMessage(Domain, Guid.Empty));
    }

    [RelayCommand]
    private void HoverEnter()
    {
        IsHover = true;
    }

    [RelayCommand]
    private void HoverLeave()
    {
        IsHover = false;
    }
}
