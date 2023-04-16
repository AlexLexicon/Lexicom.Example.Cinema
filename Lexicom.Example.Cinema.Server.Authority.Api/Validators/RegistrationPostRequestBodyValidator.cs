using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators;
public class RegistrationPostRequestBodyValidator : AbstractValidator<RegistrationPostRequestBody>
{
    public RegistrationPostRequestBodyValidator(
        EmailRuleSet emailRuleSet,
        PasswordRequirementsRuleSet passwordRequirementsRuleSet,
        NameRuleSet nameRuleSet)
    {
        RuleFor(rb => rb.Email)
            .UseRuleSet(emailRuleSet);

        RuleFor(rb => rb.Password)
            .UseRuleSet(passwordRequirementsRuleSet);

        RuleFor(rb => rb.FirstName)
            .UseRuleSet(nameRuleSet);

        RuleFor(rb => rb.LastName)
            .UseRuleSet(nameRuleSet);
    }
}
