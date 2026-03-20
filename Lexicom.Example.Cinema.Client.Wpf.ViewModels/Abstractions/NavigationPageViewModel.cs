using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
public abstract partial class NavigationPageViewModel : ObservableObject, INotificationHandler<OpenPageNotification>, INotificationHandler<ClosePageNotification>, INotificationHandler<DismissPageNotification>
{
    protected readonly IMediator _mediator;

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

    public virtual Task Handle(OpenPageNotification notification, CancellationToken cancellationToken)
    {
        if (notification.Domain == Domain)
        {
            IsSelected = notification.Id == Id;
        }

        return Task.CompletedTask;
    }

    public virtual Task Handle(ClosePageNotification notification, CancellationToken cancellationToken)
    {
        if (notification.Domain == Domain && notification.Id == Id)
        {
            IsSelected = false;
        }

        return Task.CompletedTask;
    }

    public async Task Handle(DismissPageNotification notification, CancellationToken cancellationToken)
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
        await _mediator.Publish(new HidePagesNotification());
        await _mediator.Publish(new OpenPageNotification(Domain, Id));
    }

    [RelayCommand]
    protected virtual async Task CloseAsync()
    {
        await _mediator.Publish(new HidePagesNotification());
        await _mediator.Publish(new ClosePageNotification(Domain, Id));
        await _mediator.Publish(new OpenPageNotification(Domain, Guid.Empty));
    }
}
