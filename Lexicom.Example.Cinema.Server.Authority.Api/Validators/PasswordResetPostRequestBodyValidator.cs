using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators;
public class PasswordResetPostRequestBodyValidator : AbstractValidator<PasswordResetPostRequestBody>
{
    public PasswordResetPostRequestBodyValidator(
        EmailRuleSet emailRuleSet,
        RequiredRuleSet requiredRuleSet,
        PasswordRequirementsRuleSet passwordRequirementsRuleSet)
    {
        RuleFor(rb => rb.Email)
            .UseRuleSet(emailRuleSet);

        RuleFor(rb => rb.PasswordResetToken)
            .UseRuleSet(requiredRuleSet);

        RuleFor(rb => rb.NewPassword)
            .UseRuleSet(passwordRequirementsRuleSet);
    }
}
