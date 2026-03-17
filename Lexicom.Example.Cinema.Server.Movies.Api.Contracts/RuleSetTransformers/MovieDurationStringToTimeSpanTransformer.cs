using Lexicom.Example.Cinema.Server.Movies.Api.Contracts.RuleSets;
using Lexicom.Validation;

namespace Lexicom.Example.Cinema.Server.Movies.Api.Contracts.RuleSetTransformers;

public class MovieDurationStringToTimeSpanTransformer : AbstractRuleSetTransformer<string?, TimeSpan, IRuleSetValidator<MovieDurationTimeSpanRuleSet, TimeSpan>>
{
    public MovieDurationStringToTimeSpanTransformer(IRuleSetValidator<MovieDurationTimeSpanRuleSet, TimeSpan> ruleSetValidator) : base(ruleSetValidator)
    {
    }

    public override bool TryTransform(string? property, out TimeSpan nextProperty)
    {
        throw new NotImplementedException();
    }

    public override string ErrorMessageTypeName => "Duration";
}
