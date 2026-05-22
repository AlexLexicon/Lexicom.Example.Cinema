using CommunityToolkit.Mvvm.ComponentModel;
using Lexicom.Example.Cinema.Client.Application.Models;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using Lexicom.Mvvm;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels.Abstractions;
public abstract partial class PageViewModel : DisposableObservableObject, IAsyncRecipient<OpenPageMessage>, IAsyncRecipient<HidePagesMessage>
{
    public PageViewModel(Domain domain)
    {
        Domain = domain;
    }

    public Domain Domain { get; }

    [ObservableProperty]
    public partial Guid Id { get; set; }
    [ObservableProperty]
    public partial bool IsVisible { get; set; }
    [ObservableProperty]
    public partial bool IsLoading { get; set; }

    public async Task ReceiveAsync(OpenPageMessage message, CancellationToken cancellationToken)
    {
        IsVisible = false;
        IsLoading = false;

        if (message.Domain == Domain && message.PageId != Guid.Empty)
        {
            Id = message.PageId;

            IsVisible = true;
            IsLoading = true;

            await OpenedAsync();

            IsLoading = false;
        }
    }

    public Task ReceiveAsync(HidePagesMessage message, CancellationToken cancellationToken)
    {
        IsVisible = false;

        return Task.CompletedTask;
    }

    public abstract Task OpenedAsync();
}
