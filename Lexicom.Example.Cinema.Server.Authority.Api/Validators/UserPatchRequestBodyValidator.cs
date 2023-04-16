using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators;
public class UserPatchRequestBodyValidator : AbstractValidator<UserPatchRequestBody>
{
    public UserPatchRequestBodyValidator(NameRuleSet nameRuleSet)
    {
        RuleFor(rb => rb.FirstName)
            .UseRuleSetWhenNotNull(nameRuleSet);

        RuleFor(rb => rb.LastName)
            .UseRuleSetWhenNotNull(nameRuleSet);
    }
}
