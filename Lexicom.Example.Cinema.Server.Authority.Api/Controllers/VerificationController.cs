using Lexicom.AspNetCore.Controllers.Amenities;
using Lexicom.AspNetCore.Controllers.Amenities.Extensions;
using Lexicom.Authentication.For.AspNetCore.Controllers.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts.Errors;
using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Example.Cinema.Server.Shared.Authentication;
using Lexicom.Swashbuckle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Controllers;
[Authorize]
[ApiController]
[Route("api/authority/user/verification")]
public class VerificationController : LexicomController
{
    private readonly ILogger<VerificationController> _logger;
    private readonly IVerificationService _verificationService;
    private readonly ICommunicationService _communicationService;

    public VerificationController(
        ILogger<VerificationController> logger,
        IVerificationService verificationService,
        ICommunicationService communicationService)
    {
        _logger = logger;
        _verificationService = verificationService;
        _communicationService = communicationService;
    }

    [SwaggerExample("""
    {
        "Email": "test_a@email.com", 
        "EmailConfirmationToken": "<the-confirmation-token>"
    }
    """)]
    [HttpPost("email/confirm")]
    [AllowAnonymous]
    public async Task<IActionResult> UserVerificationEmailConfirmAsync([FromBody] VerificationEmailConfirmPostRequestBody requestBody)
    {
        try
        {
            await _verificationService.ConfirmUserEmailAsync(requestBody.Email, requestBody.EmailConfirmationToken);

            return NoContent();
        }
        catch (UserDoesNotExistException e)
        {
            _logger.LogWarning(e, "Failed to confirm the email because the user with the email '{email}' does not exist.", requestBody.Email);

            //if the user does not exist, we want to return no content
            //since this is an anonymous endpoint anyone could call this
            //with any random email address trying to see if that user exists
            //and we want to protect against enumeration vulnerabilities
            return NoContent();
        }
        catch (UserEmailAlreadyConfirmedException e)
        {
            _logger.LogWarning(e, "Failed to confirm the email because the user with the email '{email}' has already confirmed their email.", requestBody.Email);

            return BadRequest()
                .FromProperty(requestBody.Email)
                .WithMessage("Already confirmed.")
                .AddCode(AuthorityErrorCodes.USER_EMAIL_CONFIRMED);
        }
        catch (EmailConfirmationTokenNotValidException e)
        {
            _logger.LogWarning(e, "Failed to confirm the email because the provided confirmation token has expired or is no longer valid.");

            return BadRequest()
                .FromProperty(requestBody.EmailConfirmationToken)
                .WithMessage("The token has expired or is not valid.")
                .AddCode(AuthorityErrorCodes.TOKEN_INVALID);
        }
    }

    [SwaggerExample("""
    {
        "Email": "test_a@email.com"
    }
    """)]
    [HttpPost("email/confirm/resend")]
    [AllowAnonymous]
    public async Task<IActionResult> UserVerificationEmailConfirmResendAsync([FromBody] VerificationEmailConfirmResendPostRequestBody requestBody)
    {
        try
        {
            await _communicationService.SendUserConfirmEmailCommunciationAsync(requestBody.Email);

            return NoContent();
        }
        catch (UserDoesNotExistException e)
        {
            _logger.LogWarning(e, "Failed to resend the confirm email because the user with the email '{email}' does not exist.", requestBody.Email);

            //if the user does not exist, we want to return no content
            //since this is an anonymous endpoint anyone could call this
            //with any random email address trying to see if that user exists
            //and we want to protect against enumeration vulnerabilities
            return NoContent();
        }
    }

    [SwaggerExample("""
    {
        "NewEmail": "test_b@email.com",
        "EmailChangeToken": "<the-change-token>"
    }
    """)]
    [HttpPost("email/change")]
    [Authorize(Policy = Policies.Permissions.Authority.Verification.EMAIL_CHANGE_POST)]
    public async Task<IActionResult> UserVerificationEmailChangeAsync([FromBody] VerificationEmailChangePostRequestBody requestBody)
    {
        Guid userId = User.GetId();
        try
        {
            await _verificationService.ChangeUserEmailAsync(userId, requestBody.NewEmail, requestBody.EmailChangeToken);

            return NoContent();
        }
        catch (UserDoesNotExistException e)
        {
            //because we are authorizing the user must exist
            throw e.ToUnreachableException();
        }
        catch (EmailChangeTokenNotValidException e)
        {
            _logger.LogWarning(e, "Failed to change the email for the user with the id '{userId}' because the provided change token has expired or is no longer valid.", userId);

            return BadRequest()
                .FromProperty(requestBody.EmailChangeToken)
                .WithMessage("The token has expired or is not valid.")
                .AddCode(AuthorityErrorCodes.TOKEN_INVALID);
        }
    }
}
