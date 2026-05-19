using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts.Email;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators.Email;

public class UserEmailChangeConfirmPostRequestBodyValidator : AbstractValidator<UserEmailChangeConfirmPostRequestBody>
{
    public UserEmailChangeConfirmPostRequestBodyValidator(
        EmailRuleSet emailRuleSet,
        RequiredRuleSet requiredRuleSet)
    {
        RuleFor(rb => rb.NewEmail)
            .UseRuleSet(emailRuleSet);

        RuleFor(rb => rb.EmailChangeToken)
            .UseRuleSet(requiredRuleSet);
    }
}
