using Lexicom.Validation;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Client.Blazor.WebAssembly.ViewModels.Extensions;
public static class ValidationServiceBuilderExtensions
{
    public static void AddViewModelRuleSets(this IValidationServiceBuilder builder)
    {
        builder.AddRuleSets<AssemblyScanMarker>();
    }
}
