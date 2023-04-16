using Lexicom.Example.Cinema.Server.Authority.Application.Options;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.Options;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Validators;
public class BrandOptionsValidator : AbstractOptionsValidator<BrandOptions>
{
    public BrandOptionsValidator(RequiredRuleSet requiredRuleSet)
    {
        RuleFor(o => o.AppName)
            .UseRuleSet(requiredRuleSet);

        RuleFor(o => o.CompanyName)
            .UseRuleSet(requiredRuleSet);
    }
}
