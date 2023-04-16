using Lexicom.EntityFramework.Identity.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Microsoft.AspNetCore.Identity;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Services;
public interface IModerationService
{
    /// <exception cref="UserDoesNotExistException"/>
    /// <exception cref="UserAlreadyLockedOutException"/>
    Task LockUserAsync(Guid userId);
    /// <exception cref="UserDoesNotExistException"/>
    /// <exception cref="UserNotLockedOutException"/>
    Task UnlockUserAsync(Guid userId);
}
public class ModerationService : IModerationService
{
    private readonly IUserService _userService;
    private readonly UserManager<User> _userManager;

    public ModerationService(
        IUserService userService,
        UserManager<User> userManager)
    {
        _userService = userService;
        _userManager = userManager;
    }

    public async Task LockUserAsync(Guid userId)
    {
        User user = await _userService.GetUserByIdAsync(userId);

        bool isLockedOut = await _userManager.IsLockedOutAsync(user);

        if (isLockedOut)
        {
            throw new UserAlreadyLockedOutException(userId);
        }

        IdentityResult SetLockoutEndDateIdentityResult = await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue.UtcDateTime);

        if (!SetLockoutEndDateIdentityResult.Succeeded)
        {
            throw new IdentityResultException(SetLockoutEndDateIdentityResult);
        }
    }

    public async Task UnlockUserAsync(Guid userId)
    {
        User user = await _userService.GetUserByIdAsync(userId);

        bool isLockedOut = await _userManager.IsLockedOutAsync(user);

        if (!isLockedOut)
        {
            throw new UserNotLockedOutException(userId);
        }

        IdentityResult SetLockoutEndDateIdentityResult = await _userManager.SetLockoutEndDateAsync(user, null);

        if (!SetLockoutEndDateIdentityResult.Succeeded)
        {
            throw new IdentityResultException(SetLockoutEndDateIdentityResult);
        }
    }
}
