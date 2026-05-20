using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts.Email;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators.Email;

public class UserEmailConfirmResendPostRequestBodyValidator : AbstractValidator<UserEmailConfirmResendPostRequestBody>
{
    public UserEmailConfirmResendPostRequestBodyValidator(EmailRuleSet emailRuleSet)
    {
        RuleFor(rb => rb.Email)
            .UseRuleSet(emailRuleSet);
    }
}
