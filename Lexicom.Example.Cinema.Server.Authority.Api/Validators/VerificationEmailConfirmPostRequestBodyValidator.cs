using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators;
public class VerificationEmailConfirmPostRequestBodyValidator : AbstractValidator<EmailConfirmPostRequestBody>
{
    public VerificationEmailConfirmPostRequestBodyValidator(
        EmailRuleSet emailRuleSet,
        RequiredRuleSet requiredRuleSet)
    {
        RuleFor(rb => rb.Email)
            .UseRuleSet(emailRuleSet);

        RuleFor(rb => rb.EmailConfirmationToken)
            .UseRuleSet(requiredRuleSet);
    }
}
