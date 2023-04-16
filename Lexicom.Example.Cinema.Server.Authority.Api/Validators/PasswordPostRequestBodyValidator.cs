using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators;
public class PasswordPostRequestBodyValidator : AbstractValidator<PasswordPostRequestBody>
{
    public PasswordPostRequestBodyValidator(
        RequiredRuleSet requiredRuleSet,
        PasswordRequirementsRuleSet passwordRequirementsRuleSet)
    {
        RuleFor(rb => rb.CurrentPassword)
            .UseRuleSet(requiredRuleSet);

        RuleFor(rb => rb.NewPassword)
            .UseRuleSet(passwordRequirementsRuleSet);
    }
}
