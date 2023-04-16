using Lexicom.AspNetCore.Controllers.Amenities;
using Lexicom.AspNetCore.Controllers.Amenities.Extensions;
using Lexicom.Authentication.For.AspNetCore.Controllers.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts.Errors;
using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Example.Cinema.Server.Shared.Authentication;
using Lexicom.Extensions.Exceptions;
using Lexicom.Swashbuckle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Controllers;
[Authorize]
[ApiController]
[Route("api/authority/user/password")]
public class PasswordController : LexicomController
{
    private readonly ILogger<PasswordController> _logger;
    private readonly IPasswordService _passwordService;
    private readonly ICommunicationService _communicationService;

    public PasswordController(
        ILogger<PasswordController> logger,
        IPasswordService passwordService,
        ICommunicationService communicationService)
    {
        _logger = logger;
        _passwordService = passwordService;
        _communicationService = communicationService;
    }

    [SwaggerExample("""
    {
        "CurrentPassword": "Password1234!",
        "NewPassword": "!4321drowssaP"
    }
    """)]
    [HttpPatch]
    [Authorize(Policy = Policies.Permissions.Authority.Password.PATCH)]
    public async Task<IActionResult> UserPasswordPatchAsync([FromBody] PasswordPostRequestBody requestBody)
    {
        Guid userId = User.GetId();
        try
        {
            await _passwordService.ChangeUserPasswordAsync(userId, requestBody.CurrentPassword, requestBody.NewPassword);

            return NoContent();
        }
        catch (UserDoesNotExistException e)
        {
            throw e.ToUnreachableException();
        }
        catch (PasswordMissingRequirementsException e)
        {
            //we want to catch missing password requirements with our IPasswordRequirementsRuleSet validator
            //so if this exception is reached we need to update that validator
            var unreachableException = e.ToUnreachableException("a password was missing requirements but not caught with our validation.");
            _logger.LogCritical(unreachableException, "Failed to reset the password for the user with tthe id '{userId}' beacause the new password did not meet the password requirements.", userId);

            return BadRequest()
                .FromProperty(requestBody.NewPassword)
                .WithMessage("Must meet the security requirements.")
                .AddCode(AuthorityErrorCodes.USER_PASSWORD_REQUIREMENTS_MISSING);
        }
        catch (PasswordIncorrectException e)
        {
            _logger.LogWarning(e, "Failed to update the password for the user with the id '{userId}' because the current password was incorrect.", userId);

            return BadRequest()
                .FromProperty(requestBody.NewPassword)
                .WithMessage("Incorrect password.")
                .AddCode(AuthorityErrorCodes.USER_PASSWORD_INCORRECT);
        }
    }

    [SwaggerExample("""
    {
        "Email": "test_a@email.com"
    }
    """)]
    [HttpPost("forgot")]
    [AllowAnonymous]
    public async Task<IActionResult> UserPasswordForgotPostAsync([FromBody] PasswordForgotPostRequestBody requestBody)
    {
        try
        {
            await _communicationService.SendUserForgotPasswordEmailAsync(requestBody.Email);

            return NoContent();
        }
        catch (UserDoesNotExistException e)
        {
            _logger.LogWarning(e, "Failed to reguest the forgot password email because the user with the email '{email}' does not exist.", requestBody.Email);

            //if the user does not exist, we want to return no content
            //since this is an anonymous endpoint anyone could call this
            //with any random email address trying to see if that user exists
            //and we want to protect against enumeration vulnerabilities
            return NoContent();
        }
    }

    [SwaggerExample("""
    {
        "Email": "test_a@email.com",
        "PasswordResetToken": "<the-reset-token>",
        "NewPassword": "Password1234!"
    }
    """)]
    [HttpPost("reset")]
    [AllowAnonymous]
    public async Task<IActionResult> UserPasswordResetPostAsync([FromBody] PasswordResetPostRequestBody requestBody)
    {
        try
        {
            await _passwordService.ResetUserPasswordAsync(requestBody.Email, requestBody.PasswordResetToken, requestBody.NewPassword);

            return NoContent();
        }
        catch (UserDoesNotExistException e)
        {
            _logger.LogWarning(e, "Failed to reset the password because the user with the email '{email}' does not exist.", requestBody.Email);

            //if the user does not exist, we want to return
            //that the password reset token is expired since
            //this is an anonymous endpoint and anyone
            //could call this with any random email
            //trying to see if the user exists
            //and we want to protect against enumeration vulnerabilities
            return BadRequest()
                .FromProperty(requestBody.PasswordResetToken)
                .WithMessage("The token has expired or is not valid.")
                .AddCode(AuthorityErrorCodes.TOKEN_INVALID);
        }
        catch (PasswordMissingRequirementsException e)
        {
            //we want to catch missing password requirements with our IPasswordRequirementsRuleSet validator
            //so if this exception is reached we need to update that validator
            var unreachableException = e.ToUnreachableException("a password was missing requirements but not caught with our validation.");
            _logger.LogCritical(unreachableException, "Failed to reset the password for the email '{email}' beacause the new password did not meet the password requirements.", requestBody.Email);

            return BadRequest()
                .FromProperty(requestBody.NewPassword)
                .WithMessage("Must meet the security requirements.")
                .AddCode(AuthorityErrorCodes.USER_PASSWORD_REQUIREMENTS_MISSING);
        }
        catch (PasswordResetTokenNotValidException e)
        {
            _logger.LogWarning(e, "Failed to reset the password because the provided reset token has expired or is no longer valid.");

            return BadRequest()
                .FromProperty(requestBody.PasswordResetToken)
                .WithMessage("The token has expired or is not valid.")
                .AddCode(AuthorityErrorCodes.TOKEN_INVALID);
        }
    }
}
