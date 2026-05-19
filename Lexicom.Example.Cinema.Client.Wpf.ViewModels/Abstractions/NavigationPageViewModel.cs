using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using Lexicom.Mvvm.Extensions;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
public abstract partial class NavigationPageViewModel : ObservableObject, INotificationHandler<OpenPageMessage>, INotificationHandler<ClosePageMessage>, INotificationHandler<DismissPageMessage>
{
    protected readonly IMessenger _mediator;

    public NavigationPageViewModel(
        Domains domain,
        Guid id,
        IMediator mediator)
    {
        _mediator = mediator;

        Domain = domain;
        Id = id;
    }

    public Domains Domain { get; }

    [ObservableProperty]
    private Guid _id;
    [ObservableProperty]
    private bool _isSelected;
    [ObservableProperty]
    private bool _isLoading;

    public virtual Task Handle(OpenPageMessage notification, CancellationToken cancellationToken)
    {
        if (notification.Domain == Domain)
        {
            IsSelected = notification.PageId == Id;
        }

        return Task.CompletedTask;
    }

    public virtual Task Handle(ClosePageMessage notification, CancellationToken cancellationToken)
    {
        if (notification.Domain == Domain && notification.PageId == Id)
        {
            IsSelected = false;
        }

        return Task.CompletedTask;
    }

    public async Task Handle(DismissPageMessage notification, CancellationToken cancellationToken)
    {
        if (notification.Domain == Domain)
        {
            await CloseAsync();
        }
    }

    public abstract Task LoadAsync();

    [RelayCommand]
    private async Task LoadedAsync()
    {
        //when we create a new navigation page it should be selected
        IsSelected = true;
        IsLoading = true;
        await LoadAsync();
        IsLoading = false;
    }

    [RelayCommand]
    protected virtual async Task SelectAsync()
    {
        await _mediator.Publish(new HidePagesMessage());
        await _mediator.ScheduleAsync(new OpenPageMessage(Domain, Id));
    }

    [RelayCommand]
    protected virtual async Task CloseAsync()
    {
        await _mediator.Publish(new HidePagesMessage());
        await _mediator.Publish(new ClosePageMessage(Domain, Id));
        await _mediator.ScheduleAsync(new OpenPageMessage(Domain, Guid.Empty));
    }
}
