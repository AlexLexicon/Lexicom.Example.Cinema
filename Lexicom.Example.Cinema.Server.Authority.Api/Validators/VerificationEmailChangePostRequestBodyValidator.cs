using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators;

public class VerificationEmailChangePostRequestBodyValidator : AbstractValidator<EmailChangePostRequestBody>
{
    public VerificationEmailChangePostRequestBodyValidator(
        EmailRuleSet emailRuleSet,
        RequiredRuleSet requiredRuleSet)
    {
        RuleFor(rb => rb.NewEmail)
            .UseRuleSet(emailRuleSet);

        RuleFor(rb => rb.EmailChangeToken)
            .UseRuleSet(requiredRuleSet);
    }
}
