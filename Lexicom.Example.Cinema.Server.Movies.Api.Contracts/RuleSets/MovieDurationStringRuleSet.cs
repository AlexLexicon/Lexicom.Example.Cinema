using FluentValidation;
using Lexicom.Validation;
using Lexicom.Validation.Amenities.Extensions;

namespace Lexicom.Example.Cinema.Server.Movies.Api.Contracts.RuleSets;

public class MovieDurationStringRuleSet : AbstractRuleSet<string?>
{
    public override void Use<T>(IRuleBuilderOptions<T, string?> ruleBuilder)
    {
        ruleBuilder
            .NotNull()
            .NotEmpty()
            .NotAllWhitespaces();
    }
}
