using FluentValidation;
using Lexicom.Validation;

namespace Lexicom.Example.Cinema.Server.Movies.Api.Contracts.RuleSets;
public class MovieReleaseDateTimeOffsetUtcRuleSet : AbstractRuleSet<DateTimeOffset>
{
    private static DateTimeOffset MinimumDateTimeOffset { get; } = new DateTimeOffset(1888, 1, 1, 1, 1, 1, TimeSpan.Zero);

    public override void Use<T>(IRuleBuilderOptions<T, DateTimeOffset> ruleBuilder)
    {
        ruleBuilder
            .GreaterThan(MinimumDateTimeOffset);
    }
}
