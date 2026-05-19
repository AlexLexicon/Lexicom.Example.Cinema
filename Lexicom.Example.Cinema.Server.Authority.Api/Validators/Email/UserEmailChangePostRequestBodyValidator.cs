using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts.Email;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators.Email;
public class UserEmailChangePostRequestBodyValidator : AbstractValidator<UserEmailChangePostRequestBody>
{
    public UserEmailChangePostRequestBodyValidator(EmailRuleSet emailRuleSet)
    {
        RuleFor(rb => rb.NewEmail)
            .UseRuleSet(emailRuleSet);
    }
}
