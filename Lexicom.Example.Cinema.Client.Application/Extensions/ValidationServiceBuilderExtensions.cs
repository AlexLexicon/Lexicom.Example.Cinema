using Lexicom.Example.Cinema.Server.Movies.Api.Contracts.Extensions;
using Lexicom.Validation;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Client.Application.Extensions;
public static class ValidationServiceBuilderExtensions
{
    public static void AddClientApplication(this IValidationServiceBuilder builder)
    {
        builder.AddAmenities();
        builder.AddMoviesApiRuleSets();
        builder.AddValidators<AssemblyScanMarker>();
    }
}
