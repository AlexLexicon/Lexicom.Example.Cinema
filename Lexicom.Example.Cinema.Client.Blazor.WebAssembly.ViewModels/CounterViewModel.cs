using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Example.Cinema.Client.Blazor.WebAssembly.ViewModels.RuleSets;
using Lexicom.Validation;

namespace Lexicom.Example.Cinema.Client.Blazor.WebAssembly.ViewModels;
public partial class CounterViewModel : ObservableObject
{
    public CounterViewModel(IRuleSetValidator<MyRuleSet, string?> ruleSetValidator)
    {
        _myValidator = ruleSetValidator;

        Title = "Yo dog whats up";

        Errors = new List<string>();
    }

    [ObservableProperty]
    private IRuleSetValidator<MyRuleSet, string?> _myValidator;
    [ObservableProperty]
    private string? _title;
    [ObservableProperty]
    private int _count;
    [ObservableProperty]
    private string? _text;
    [ObservableProperty]
    private IReadOnlyList<string> _errors;

    [RelayCommand]
    private async Task ValidateText()
    {
        await MyValidator.ValidateAsync(Text);

        Errors = MyValidator.ValidationErrors;
    }

    [RelayCommand]
    private void Increment()
    {
        Count++;
    }
}
