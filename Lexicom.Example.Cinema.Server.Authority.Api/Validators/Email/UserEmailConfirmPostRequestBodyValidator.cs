using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts.Email;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators.Email;

public class UserEmailConfirmPostRequestBodyValidator : AbstractValidator<UserEmailConfirmPostRequestBody>
{
    public UserEmailConfirmPostRequestBodyValidator(
        EmailRuleSet emailRuleSet,
        RequiredRuleSet requiredRuleSet)
    {
        RuleFor(rb => rb.Email)
            .UseRuleSet(emailRuleSet);

        RuleFor(rb => rb.EmailConfirmationToken)
            .UseRuleSet(requiredRuleSet);
    }
}
