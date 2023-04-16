using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators;
public class PasswordForgotPostRequestBodyValidator : AbstractValidator<PasswordForgotPostRequestBody>
{
    public PasswordForgotPostRequestBodyValidator(EmailRuleSet emailRuleSet)
    {
        RuleFor(rb => rb.Email)
            .UseRuleSet(emailRuleSet);
    }
}
