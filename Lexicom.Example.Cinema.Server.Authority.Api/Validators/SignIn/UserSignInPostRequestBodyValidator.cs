using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts.SignIn;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators.SignIn;
public class UserSignInPostRequestBodyValidator : AbstractValidator<UserSignInPostRequestBody>
{
    public UserSignInPostRequestBodyValidator(
        EmailRuleSet emailRuleSet,
        RequiredRuleSet requiredRuleSet)
    {
        RuleFor(rb => rb.Email)
            .UseRuleSet(emailRuleSet);

        RuleFor(rb => rb.Password)
            .UseRuleSet(requiredRuleSet);
    }
}
