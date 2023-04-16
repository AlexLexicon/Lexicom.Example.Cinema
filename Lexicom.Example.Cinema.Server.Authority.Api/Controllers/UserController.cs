using Lexicom.AspNetCore.Controllers.Amenities;
using Lexicom.Authentication.For.AspNetCore.Controllers.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Api.Contracts;
using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Example.Cinema.Server.Shared.Authentication;
using Lexicom.Swashbuckle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.Example.Cinema.Server.Authority.Api.Controllers;
[Authorize]
[ApiController]
[Route("api/authority/user")]
public class UserController : LexicomController
{
    private readonly IUserService _userService;
    private readonly ICommunicationService _communicationService;

    public UserController(
        IUserService userService,
        ICommunicationService communicationService)
    {
        _userService = userService;
        _communicationService = communicationService;
    }

    [HttpGet]
    [Authorize(Policy = Policies.Permissions.Authority.User.GET)]
    public async Task<IActionResult> UserGetAsync()
    {
        Guid userId = User.GetId();

        try
        {
            ComprehensiveUser comprehensiveUser = await _userService.GetComprehensiveUserAsync(userId);

            return Ok(new UserGetResponseBody
            {
                Id = comprehensiveUser.Id,
                Email = comprehensiveUser.Email,
                FirstName = comprehensiveUser.FirstName,
                LastName = comprehensiveUser.LastName,
                CreatedDateTimeOffsetUtc = comprehensiveUser.CreatedDateTimeOffset,
                Roles = comprehensiveUser.Roles.Select(cr => new UserGetResponseBodyRole
                {
                    Id = cr.Id,
                    Name = cr.Name,
                    Permissions = cr.Permissions.ToList(),
                }).ToList()
            });
        }
        catch (UserDoesNotExistException e)
        {
            //because we are authorizing the user must exist
            throw e.ToUnreachableException();
        }
    }

    [SwaggerExample("""
    {
        "FirstName": null,
        "LastName": null
    }
    """)]
    [HttpPatch]
    [Authorize(Policy = Policies.Permissions.Authority.User.PATCH)]
    public async Task<IActionResult> UserPatchAsync([FromBody] UserPatchRequestBody requestBody)
    {
        Guid userId = User.GetId();
        try
        {
            await _userService.UpdateUserAsync(userId, requestBody.FirstName, requestBody.LastName);

            return NoContent();
        }
        catch (UserDoesNotExistException e)
        {
            //because we are authorizing the user must exist
            throw e.ToUnreachableException();
        }
    }

    [SwaggerExample("""
    {
        "NewEmail": "test_b@email.com"
    }
    """)]
    [HttpPatch("email")]
    [Authorize(Policy = Policies.Permissions.Authority.User.EMAIL_PATCH)]
    public async Task<IActionResult> UserEmailPatchAsync([FromBody] UserEmailPostRequestBody requestBody)
    {
        Guid userId = User.GetId();
        try
        {
            await _communicationService.SendChangeEmailCommunicationAsync(userId, requestBody.NewEmail);

            return NoContent();
        }
        catch (UserDoesNotExistException e)
        {
            //because we are authorizing the user must exist
            throw e.ToUnreachableException();
        }
    }
}
