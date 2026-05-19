using CommunityToolkit.Mvvm.ComponentModel;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
public abstract partial class PageViewModel : ObservableObject, INotificationHandler<OpenPageMessage>, INotificationHandler<HidePagesMessage>
{
    public PageViewModel(Domains domain)
    {
        Domain = domain;
    }

    public Domains Domain { get; }

    [ObservableProperty]
    private Guid _id;
    [ObservableProperty]
    private bool _isVisible;
    [ObservableProperty]
    private bool _isLoading;

    public async Task Handle(OpenPageMessage notification, CancellationToken cancellationToken)
    {
        IsVisible = false;
        IsLoading = false;

        if (notification.Domain == Domain && notification.PageId != Guid.Empty)
        {
            Id = notification.PageId;

            IsVisible = true;
            IsLoading = true;

            await OpenedAsync();

            IsLoading = false;
        }
    }

    public Task Handle(HidePagesMessage notification, CancellationToken cancellationToken)
    {
        IsVisible = false;

        return Task.CompletedTask;
    }

    public abstract Task OpenedAsync();
}
