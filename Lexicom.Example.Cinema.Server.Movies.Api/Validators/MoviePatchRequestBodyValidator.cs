using FluentValidation;
using Lexicom.Example.Cinema.Server.Movies.Api.Contracts;
using Lexicom.Example.Cinema.Server.Movies.Api.Contracts.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Movies.Api.Validators;
public class MoviePatchRequestBodyValidator : AbstractValidator<MoviePatchRequestBody>
{
    public MoviePatchRequestBodyValidator(
        MovieTitleStringRuleSet movieTitleRuleSet,
        MovieDurationTimeSpanRuleSet movieDurationRuleSet,
        MovieReleaseDateTimeOffsetRuleSet movieReleaseDateTimeOffsetUtcRuleSet,
        MovieSynopsisStringRuleSet movieSynopsisRuleSet)
    {
        RuleFor(rb => rb.NewTitle)
            .UseRuleSetWhenNotNull(movieTitleRuleSet);

        RuleFor(rb => rb.NewDuration)
            .UseRuleSetWhenNotNull(movieDurationRuleSet);

        RuleFor(rb => rb.NewReleaseDateTimeOffsetUtc)
            .UseRuleSetWhenNotNull(movieReleaseDateTimeOffsetUtcRuleSet);

        RuleFor(rb => rb.NewSynopsis)
            .UseRuleSet(movieSynopsisRuleSet);
    }
}
