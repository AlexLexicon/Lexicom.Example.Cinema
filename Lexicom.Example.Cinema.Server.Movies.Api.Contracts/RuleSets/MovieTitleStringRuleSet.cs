using FluentValidation;
using Lexicom.Validation;
using Lexicom.Validation.Amenities.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Movies.Api.Contracts.RuleSets;
public class MovieTitleStringRuleSet : AbstractRuleSet<string?>
{
    private readonly RequiredRuleSet _requiredRuleSet;

    public MovieTitleStringRuleSet(RequiredRuleSet requiredRuleSet)
    {
        _requiredRuleSet = requiredRuleSet;
    } 

    public override void Use<T>(IRuleBuilderOptions<T, string?> ruleBuilder)
    {
        ruleBuilder
            .UseRuleSet(_requiredRuleSet)
            .Length(1, 256);
    }
}
