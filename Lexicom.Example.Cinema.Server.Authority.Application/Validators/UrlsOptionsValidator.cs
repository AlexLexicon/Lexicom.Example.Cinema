using Lexicom.Example.Cinema.Server.Authority.Application.Options;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.Options;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Validators;

public class UrlsOptionsValidator : AbstractOptionsValidator<UrlsOptions>
{
    public UrlsOptionsValidator(RequiredRuleSet requiredRuleSet)
    {
        RuleFor(o => o.ConfirmationEmailUrl)
            .UseRuleSet(requiredRuleSet);

        RuleFor(o => o.ChangeEmailUrl)
            .UseRuleSet(requiredRuleSet);

        RuleFor(o => o.ForgotPasswordEmailUrl)
            .UseRuleSet(requiredRuleSet);
    }
}
