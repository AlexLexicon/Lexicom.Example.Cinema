using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Example.Cinema.Client.Blazor.WebAssembly.Options;
using Microsoft.Extensions.Options;

namespace Lexicom.Example.Cinema.Client.Blazor.WebAssembly.ViewModels;
public partial class CounterViewModel : ObservableObject
{
    public CounterViewModel(IOptions<MyOptions> options)
    {
        Title = options.Value.CounterTitle;
    }

    [ObservableProperty]
    private string? _title;
    [ObservableProperty]
    private int _count;

    [RelayCommand]
    private void Increment()
    {
        Count++;
    }
}
