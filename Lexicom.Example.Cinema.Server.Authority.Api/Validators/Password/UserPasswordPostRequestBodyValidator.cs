using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts.Password;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators.Password;

public class UserPasswordPostRequestBodyValidator : AbstractValidator<UserPasswordPostRequestBody>
{
    public UserPasswordPostRequestBodyValidator(
        RequiredRuleSet requiredRuleSet,
        PasswordRequirementsRuleSet passwordRequirementsRuleSet)
    {
        RuleFor(rb => rb.CurrentPassword)
            .UseRuleSet(requiredRuleSet);

        RuleFor(rb => rb.NewPassword)
            .UseRuleSet(passwordRequirementsRuleSet);
    }
}
