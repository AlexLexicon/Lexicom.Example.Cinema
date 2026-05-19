using Lexicom.EntityFramework.Identity.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Database.Entities;
using Microsoft.AspNetCore.Identity;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Services;

public interface IModerationService
{
    /// <exception cref="UserDoesNotExistException"/>
    /// <exception cref="UserAlreadyLockedOutException"/>
    Task LockUserAsync(Guid userId);
    /// <exception cref="UserDoesNotExistException"/>
    Task UnlockUserAsync(Guid userId);
}
public class ModerationService : IModerationService
{
    private readonly IUserService _userService;
    private readonly UserManager<User> _userManager;
    private readonly IRefreshTokenEntryService _refreshTokenEntriesService;

    public ModerationService(
        IUserService userService,
        UserManager<User> userManager,
        IRefreshTokenEntryService refreshTokenEntriesService)
    {
        _userService = userService;
        _userManager = userManager;
        _refreshTokenEntriesService = refreshTokenEntriesService;
    }

    public async Task LockUserAsync(Guid userId)
    {
        User user = await _userService.GetUserByIdAsync(userId);

        bool isLockedOut = await _userManager.IsLockedOutAsync(user);

        if (isLockedOut)
        {
            throw new UserAlreadyLockedOutException(userId);
        }

        IdentityResult setLockoutEndDateIdentityResult = await _userManager.SetLockoutEndDateAsync(user, lockoutEnd: DateTimeOffset.MaxValue.UtcDateTime);

        if (!setLockoutEndDateIdentityResult.Succeeded)
        {
            throw new IdentityResultException(setLockoutEndDateIdentityResult);
        }

        //if a user is locked out we should delete their refresh token entry(s)
        await _refreshTokenEntriesService.RemoveRefreshTokenEntriesAsync(user.Id);
    }

    public async Task UnlockUserAsync(Guid userId)
    {
        User user = await _userService.GetUserByIdAsync(userId);

        IdentityResult setLockoutEndDateIdentityResult = await _userManager.SetLockoutEndDateAsync(user, lockoutEnd: null);

        if (!setLockoutEndDateIdentityResult.Succeeded)
        {
            throw new IdentityResultException(setLockoutEndDateIdentityResult);
        }
    }
}
