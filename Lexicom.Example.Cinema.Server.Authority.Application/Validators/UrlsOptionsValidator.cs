using FluentValidation;
using Lexicom.Example.Cinema.Server.Authority.Application.Options;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.Options;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Validators;
public class UrlsOptionsValidator : AbstractOptionsValidator<UrlsOptions>
{
    public UrlsOptionsValidator()
    {
        RuleFor(o => o.ConfirmationEmailUrl)
            .NotNull()
            .NotSimplyEmpty()
            .NotAllWhitespaces();

        RuleFor(o => o.ForgotPasswordEmailUrl)
            .NotNull()
            .NotSimplyEmpty()
            .NotAllWhitespaces();
    }
}
