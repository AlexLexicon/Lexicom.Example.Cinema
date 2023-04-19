using FluentValidation;
using Lexicom.Example.Cinema.Client.Blazor.WebAssembly.Options;
using Lexicom.Validation.Options;

namespace Lexicom.Example.Cinema.Client.Blazor.WebAssembly.Validators;

public class MyOptionsValidator : AbstractOptionsValidator<MyOptions>
{
    public MyOptionsValidator()
    {
        RuleFor(o => o.CounterTitle)
            .NotNull();
    }
}
