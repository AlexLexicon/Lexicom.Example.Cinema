using Lexicom.Validation;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Movies.Api.Contracts.Extensions;
public static class ValidationServiceBuilderExtensions
{
    public static void AddMoviesApiRuleSets(this IValidationServiceBuilder builder)
    {
        builder.AddRuleSets<AssemblyScanMarker>();
    }
}
