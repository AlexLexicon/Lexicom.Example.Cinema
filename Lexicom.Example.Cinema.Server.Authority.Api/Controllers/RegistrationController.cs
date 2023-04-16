using Lexicom.AspNetCore.Controllers.Amenities;
using Lexicom.AspNetCore.Controllers.Amenities.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts.Errors;
using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Extensions.Exceptions;
using Lexicom.Swashbuckle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Controllers;
[Authorize]
[ApiController]
[Route("api/authority/user/registration")]
public class RegistrationController : LexicomController
{
    private readonly ILogger<RegistrationController> _logger;
    private readonly IRegistrationService _registrationService;

    public RegistrationController(
        ILogger<RegistrationController> logger,
        IRegistrationService registrationService)
    {
        _logger = logger;
        _registrationService = registrationService;
    }

    [SwaggerExample("""
    {
        "Email": "test_a@email.com", 
        "Password": "Password1234!",
        "FirstName": "testFirstName",
        "LastName": "testLastName"
    }
    """)]
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> UserRegistrationPostAsync([FromBody] RegistrationPostRequestBody requestBody)
    {
        try
        {
            await _registrationService.RegisterUserAsync(requestBody.Email, requestBody.Password, requestBody.FirstName, requestBody.LastName);

            return NoContent();
        }
        catch (EmailAlreadyInUseException e)
        {
            _logger.LogWarning(e, "Failed to register the user because the email '{email}' is already in use.", requestBody.Email);

            return BadRequest()
                .FromProperty(requestBody.Email)
                .WithMessage("Must not already been in use.")
                .AddCode(AuthorityErrorCodes.USER_EMAIL_UNAVAILABLE);
        }
        catch (EmailNotValidException e)
        {
            //we want to catch invalid emails with our IEmailRuleSet validator
            //so if this exception is reached we need to update that validator
            var unreachableException = e.ToUnreachableException($"The email '{requestBody.Email}' was not valid but not caught with our validation.");
            _logger.LogCritical(unreachableException, "Failed to register the user because the provided email '{email}' is not valid.", requestBody.Email);

            return BadRequest()
                .FromProperty(requestBody.Email)
                .WithMessage("was not valid.")
                .AddCode(AuthorityErrorCodes.USER_EMAIL_INVALID);
        }
        catch (PasswordMissingRequirementsException e)
        {
            _logger.LogWarning(e, "Failed to register the user because the provided password did not meet all of the security requirements.");

            return BadRequest()
                .FromProperty(requestBody.Password)
                .WithMessage("Must meet the security requirements.")
                .AddCode(AuthorityErrorCodes.USER_PASSWORD_REQUIREMENTS_MISSING);
        }
    }
}
