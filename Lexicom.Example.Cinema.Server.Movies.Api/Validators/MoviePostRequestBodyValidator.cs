using FluentValidation;
using Lexicom.Example.Cinema.Server.Movies.Api.Contracts;
using Lexicom.Example.Cinema.Server.Movies.Api.Contracts.RuleSets;
using Lexicom.Validation.Extensions;

namespace Lexicom.Example.Cinema.Server.Movies.Api.Validators;
public class MoviePostRequestBodyValidator : AbstractValidator<MoviePostRequestBody>
{
    public MoviePostRequestBodyValidator(
        MovieTitleStringRuleSet movieTitleRuleSet,
        MovieDurationTimeSpanRuleSet movieDurationRuleSet,
        MovieReleaseDateTimeOffsetRuleSet movieReleaseDateTimeOffsetUtcRuleSet,
        MovieSynopsisStringRuleSet movieSynopsisRuleSet)
    {
        RuleFor(rb => rb.Title)
            .UseRuleSet(movieTitleRuleSet);

        RuleFor(rb => rb.Duration)
            .UseRuleSet(movieDurationRuleSet);

        RuleFor(rb => rb.ReleaseDateTimeOffsetUtc)
            .UseRuleSet(movieReleaseDateTimeOffsetUtcRuleSet);

        RuleFor(rb => rb.Synopsis)
            .UseRuleSet(movieSynopsisRuleSet);
    }
}
