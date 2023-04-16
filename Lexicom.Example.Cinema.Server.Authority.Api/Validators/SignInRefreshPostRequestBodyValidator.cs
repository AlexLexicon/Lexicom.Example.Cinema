using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators;

public class SignInRefreshPostRequestBodyValidator : AbstractValidator<SignInRefreshPostRequestBody>
{
    public SignInRefreshPostRequestBodyValidator(RequiredRuleSet requiredRuleSet)
    {
        RuleFor(rb => rb.AccessBearerToken)
            .UseRuleSet(requiredRuleSet);

        RuleFor(rb => rb.RefreshBearerToken)
            .UseRuleSet(requiredRuleSet);
    }
}
