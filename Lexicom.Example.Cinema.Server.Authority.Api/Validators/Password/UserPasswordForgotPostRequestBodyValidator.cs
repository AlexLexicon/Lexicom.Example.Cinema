using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts.Password;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators.Password;
public class UserPasswordForgotPostRequestBodyValidator : AbstractValidator<UserPasswordForgotPostRequestBody>
{
    public UserPasswordForgotPostRequestBodyValidator(EmailRuleSet emailRuleSet)
    {
        RuleFor(rb => rb.Email)
            .UseRuleSet(emailRuleSet);
    }
}
