using FluentValidation;
using Lexicom.Validation;

namespace Lexicom.Example.Cinema.Server.Movies.Api.Contracts.RuleSets;
public class MovieDurationRuleSet : AbstractRuleSet<TimeSpan>
{
    public override void Use<T>(IRuleBuilderOptions<T, TimeSpan> ruleBuilder)
    {
        ruleBuilder
            .GreaterThan(TimeSpan.Zero);
    }
}
