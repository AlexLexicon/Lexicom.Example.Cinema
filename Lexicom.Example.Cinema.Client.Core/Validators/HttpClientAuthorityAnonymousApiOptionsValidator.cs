using Lexicom.Example.Cinema.Client.Core.Options;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.Options;

namespace Lexicom.Example.Cinema.Client.Core.Validators;
public class HttpClientAuthorityAnonymousApiOptionsValidator : AbstractOptionsValidator<HttpClientAuthorityAnonymousApiOptions>
{
    public HttpClientAuthorityAnonymousApiOptionsValidator(RequiredRuleSet requiredRuleSet)
    {
        RuleFor(o => o.BaseAddress)
            .UseRuleSet(requiredRuleSet);
    }
}
