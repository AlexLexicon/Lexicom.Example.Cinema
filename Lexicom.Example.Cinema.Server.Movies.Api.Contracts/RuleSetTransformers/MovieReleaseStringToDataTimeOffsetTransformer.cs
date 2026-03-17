using Lexicom.Example.Cinema.Server.Movies.Api.Contracts.RuleSets;
using Lexicom.Validation;

namespace Lexicom.Example.Cinema.Server.Movies.Api.Contracts.RuleSetTransformers;

public class MovieReleaseStringToDataTimeOffsetTransformer : AbstractRuleSetTransformer<string?, DateTimeOffset, IRuleSetValidator<MovieReleaseDateTimeOffsetRuleSet, DateTimeOffset>>
{
    public MovieReleaseStringToDataTimeOffsetTransformer(IRuleSetValidator<MovieReleaseDateTimeOffsetRuleSet, DateTimeOffset> ruleSetValidator) : base(ruleSetValidator)
    {
    }

    public override bool TryTransform(string? property, out DateTimeOffset nextProperty)
    {
        return DateTimeOffset.TryParse(property, out nextProperty);
    }

    public override string ErrorMessageTypeName => "Date";
}