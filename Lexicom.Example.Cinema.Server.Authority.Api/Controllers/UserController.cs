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

    public UserController(IUserService userService)
    {
        _userService = userService;
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
}
