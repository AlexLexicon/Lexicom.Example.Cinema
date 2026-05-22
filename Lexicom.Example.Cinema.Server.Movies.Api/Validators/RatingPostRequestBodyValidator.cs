using FluentValidation;
using Lexicom.Example.Cinema.Server.Movies.Api.Contracts.Review;
using Lexicom.Example.Cinema.Server.Movies.Api.Contracts.Validation.RuleSets;

namespace Lexicom.Example.Cinema.Server.Movies.Api.Validators;

public class RatingPostRequestBodyValidator : AbstractValidator<ReviewPostRequestBody>
{
    public RatingPostRequestBodyValidator(
        MovieTitleStringRuleSet movieTitleRuleSet,
        MovieDurationTimeSpanRuleSet movieDurationRuleSet,
        MovieReleaseDateTimeOffsetRuleSet movieReleaseDateTimeOffsetUtcRuleSet,
        MovieSynopsisStringRuleSet movieSynopsisRuleSet)
    {
        //RuleFor(rb => rb.Rating)
        //    .UseRuleSet(movieTitleRuleSet);

        //RuleFor(rb => rb.Text)
        //    .UseRuleSet(movieDurationRuleSet);
    }
}
