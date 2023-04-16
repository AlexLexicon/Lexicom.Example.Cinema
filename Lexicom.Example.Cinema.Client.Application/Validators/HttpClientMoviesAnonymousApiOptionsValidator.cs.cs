using Lexicom.Example.Cinema.Client.Application.Options;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.Options;

namespace Lexicom.Example.Cinema.Client.Application.Validators;
public class HttpClientMoviesAnonymousApiOptionsValidator : AbstractOptionsValidator<HttpClientMoviesAnonymousApiOptions>
{
    public HttpClientMoviesAnonymousApiOptionsValidator(RequiredRuleSet requiredRuleSet)
    {
        RuleFor(o => o.BaseAddress)
            .UseRuleSet(requiredRuleSet);
    }
}
