using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators;
public class UserEmailPostRequestBodyValidator : AbstractValidator<EmailPostRequestBody>
{
    public UserEmailPostRequestBodyValidator(EmailRuleSet emailRuleSet)
    {
        RuleFor(rb => rb.NewEmail)
            .UseRuleSet(emailRuleSet);
    }
}
