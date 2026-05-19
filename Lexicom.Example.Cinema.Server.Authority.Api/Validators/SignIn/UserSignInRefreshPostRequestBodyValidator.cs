using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts.SignIn;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators.SignIn;

public class UserSignInRefreshPostRequestBodyValidator : AbstractValidator<UserSignInRefreshPostRequestBody>
{
    public UserSignInRefreshPostRequestBodyValidator(RequiredRuleSet requiredRuleSet)
    {
        RuleFor(rb => rb.AccessBearerToken)
            .UseRuleSet(requiredRuleSet);

        RuleFor(rb => rb.RefreshBearerToken)
            .UseRuleSet(requiredRuleSet);
    }
}
