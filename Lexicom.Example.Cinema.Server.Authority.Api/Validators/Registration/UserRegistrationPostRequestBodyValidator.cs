using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts.Registration;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Validators.Registration;

public class UserRegistrationPostRequestBodyValidator : AbstractValidator<UserRegistrationPostRequestBody>
{
    public UserRegistrationPostRequestBodyValidator(
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
