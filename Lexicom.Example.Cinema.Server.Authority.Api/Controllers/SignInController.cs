using Lexicom.AspNetCore.Controllers.Amenities;
using Lexicom.AspNetCore.Controllers.Amenities.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts.SignIn;
using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Scalar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/authority/user/signin")]
public class SignInController : LexicomController
{
    private readonly ILogger<SignInController> _logger;
    private readonly ISignInService _signInService;

    public SignInController(
        ILogger<SignInController> logger,
        ISignInService signInService)
    {
        _logger = logger;
        _signInService = signInService;
    }

    [ScalarDefaultRequestBody("""
    {
        "Email": "test_a@email.com",
        "Password": "Password1234!"
    }
    """)]
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> UserSignInPostAsync([FromBody] UserSignInPostRequestBody requestBody)
    {
        try
        {
            SignIn signIn = await _signInService.SignInUserAsync(requestBody.Email, requestBody.Password);

            return Ok(new UserSignInPostResponseBody
            {
                AccessBearerToken = signIn.AccessToken,
                RefreshBearerToken = signIn.RefreshToken,
            });
        }
        catch (UserDoesNotExistException e)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning(e, "Failed to sign-in the user with the email '{email}' because they did not exist.", requestBody.Email);
            }

            return BadRequest()
                .FromProperty(requestBody.Email)
                .WithMessage("Incorrect email or password.")
                .FromProperty(requestBody.Password)
                .WithMessage("Incorrect email or password.")
                .AddCode(AuthorityErrorCodes.USER_CREDENTIALS_INCORRECT);
        }
        catch (UserLockedOutException e)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning(e, "Failed to sign-in the user with the email '{email}' because they are locked out.", requestBody.Email);
            }

            return Forbid()
                .FromKey("User")
                .WithMessage("Locked out.")
                .AddCode(AuthorityErrorCodes.USER_MODERATION_LOCKED);
        }
        catch (UserNotVerifiedException e)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning(e, "Failed to sign-in the user with the email '{email}' because they are not verified.", requestBody.Email);
            }

            return Forbid()
                .FromKey("User")
                .WithMessage("Not verified.")
                .AddCode(AuthorityErrorCodes.USER_VERIFICATION_INCOMPLETE);
        }
        catch (PasswordIncorrectException e)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning(e, "Failed to sign-in the user with the email '{email}' because the password was incorrect.", requestBody.Email);
            }

            return BadRequest()
                .FromProperty(requestBody.Email)
                .WithMessage("Incorrect email or password.")
                .FromProperty(requestBody.Password)
                .WithMessage("Incorrect email or password.")
                .AddCode(AuthorityErrorCodes.USER_CREDENTIALS_INCORRECT);
        }
    }

    [ScalarDefaultRequestBody("""
    {
        "AccessBearerToken": "<the-access-token>",
        "RefreshBearerToken": "<the-refresh-token>"
    }
    """)]
    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<IActionResult> UserSignInRefreshPostAsync([FromBody] UserSignInRefreshPostRequestBody requestBody)
    {
        try
        {
            SignIn signIn = await _signInService.RefreshUserAsync(requestBody.AccessBearerToken, requestBody.RefreshBearerToken);

            return Ok(new UserSignInRefreshPostResponseBody
            {
                AccessBearerToken = signIn.AccessToken,
                RefreshBearerToken = signIn.RefreshToken,
            });
        }
        /*
         * we should not disclose which of the tokens
         * is actually invalid to the client
         * to protect from malicious investigation
         */
        catch (AccessBearerTokenNotValidException e)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning(e, "Failed to refresh the user because the access token was not valid.");
            }

            return BadRequest()
                .FromProperty(requestBody.AccessBearerToken)
                .WithMessage("The access token or refresh token is not valid.")
                .FromProperty(requestBody.RefreshBearerToken)
                .WithMessage("The access token or refresh token is not valid.")
                .AddCode(AuthorityErrorCodes.TOKEN_INVALID);
        }
        catch (RefreshBearerTokenNotValidException e)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning(e, "Failed to refresh the user because the refresh token was not valid.");
            }

            return BadRequest()
                .FromProperty(requestBody.AccessBearerToken)
                .WithMessage("The access token or refresh token is not valid.")
                .FromProperty(requestBody.RefreshBearerToken)
                .WithMessage("The access token or refresh token is not valid.")
                .AddCode(AuthorityErrorCodes.TOKEN_INVALID);
        }
        catch (RefreshTokenDoesNotExistException e)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning(e, "Failed to refresh the user because the refresh token did not exist. This usually would mean this refresh token had already been used.");
            }

            return BadRequest()
                .FromProperty(requestBody.AccessBearerToken)
                .WithMessage("The access token or refresh token is not valid.")
                .FromProperty(requestBody.RefreshBearerToken)
                .WithMessage("The access token or refresh token is not valid.")
                .AddCode(AuthorityErrorCodes.TOKEN_INVALID);
        }
        catch (RefreshTokenAccessTokenJtiMismatchException e)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning(e, "Failed to refresh the user because the refresh token's access token jti did not match the provided access token's jti. This usually would mean someone passed a new access token with a refresh token from a previous signin.");
            }

            return BadRequest()
                .FromProperty(requestBody.AccessBearerToken)
                .WithMessage("The access token or refresh token is not valid.")
                .FromProperty(requestBody.RefreshBearerToken)
                .WithMessage("The access token or refresh token is not valid.")
                .AddCode(AuthorityErrorCodes.TOKEN_INVALID);
        }
        catch (RefreshTokenAccessTokenSubjectMismatchException e)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning(e, "Failed to refresh the user because the refresh token's subject was different from access token's subject.");
            }

            return BadRequest()
                .FromProperty(requestBody.AccessBearerToken)
                .WithMessage("The access token or refresh token is not valid.")
                .FromProperty(requestBody.RefreshBearerToken)
                .WithMessage("The access token or refresh token is not valid.")
                .AddCode(AuthorityErrorCodes.TOKEN_INVALID);
        }
        catch (RefreshTokenUserMismatchException e)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning(e, "Failed to refresh the user because the refresh token's subject was different from access token's subject.");
            }

            return BadRequest()
                .FromProperty(requestBody.AccessBearerToken)
                .WithMessage("The access token or refresh token is not valid.")
                .FromProperty(requestBody.RefreshBearerToken)
                .WithMessage("The access token or refresh token is not valid.")
                .AddCode(AuthorityErrorCodes.TOKEN_INVALID);
        }
        catch (RefreshTokenExpiredException e)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning(e, "Failed to refresh the user because the refresh token has expired.");
            }

            return BadRequest()
                .FromProperty(requestBody.AccessBearerToken)
                .WithMessage("The access token or refresh token is not valid.")
                .FromProperty(requestBody.RefreshBearerToken)
                .WithMessage("The access token or refresh token is not valid.")
                .AddCode(AuthorityErrorCodes.TOKEN_INVALID);
        }
        catch (UserDoesNotExistException e)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning(e, "Failed to refresh the user because the user does not exist.");
            }

            return BadRequest()
                .FromProperty(requestBody.AccessBearerToken)
                .WithMessage("The access token or refresh token is not valid.")
                .FromProperty(requestBody.RefreshBearerToken)
                .WithMessage("The access token or refresh token is not valid.")
                .AddCode(AuthorityErrorCodes.TOKEN_INVALID);
        }
        catch (UserLockedOutException e)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning(e, "Failed to refresh the user because the user is locked out.");
            }

            return Forbid()
               .FromKey("User")
               .WithMessage("Locked out.")
               .AddCode(AuthorityErrorCodes.USER_MODERATION_LOCKED);
        }
        catch (UserNotVerifiedException e)
        {
            if (_logger.IsEnabled(LogLevel.Warning))
            {
                _logger.LogWarning(e, "Failed to refresh the user because the user is not verified.");
            }

            return Forbid()
                .FromKey("User")
                .WithMessage("Not verified.")
                .AddCode(AuthorityErrorCodes.USER_VERIFICATION_INCOMPLETE);
        }
    }
}
