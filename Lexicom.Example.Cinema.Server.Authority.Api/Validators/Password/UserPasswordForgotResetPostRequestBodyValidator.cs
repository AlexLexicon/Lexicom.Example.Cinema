using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts.Password;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators.Password;

public class UserPasswordForgotResetPostRequestBodyValidator : AbstractValidator<UserPasswordForgotResetPostRequestBody>
{
    public UserPasswordForgotResetPostRequestBodyValidator(
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
