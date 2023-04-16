using Lexicom.DependencyInjection.Primitives;
using Lexicom.EntityFramework.Identity.Exceptions;
using Lexicom.EntityFramework.Identity.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Microsoft.AspNetCore.Identity;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Services;
public interface IVerificationService
{
    /// <exception cref="UserDoesNotExistException"/>
    Task<string> CreateUserEmailConfirmationTokenAsync(Guid userId);
    /// <exception cref="UserDoesNotExistException"/>
    /// <exception cref="UserEmailAlreadyConfirmedException"/>
    /// <exception cref="EmailConfirmationTokenNotValidException"/>
    Task ConfirmUserEmailAsync(string email, string emailConfirmationToken);
    /// <exception cref="UserDoesNotExistException"/>
    Task<string> CreateUserEmailChangeTokenAsync(Guid userId, string newEmail);
    /// <exception cref="UserDoesNotExistException"/>
    /// <exception cref="EmailChangeTokenNotValidException"/>
    Task ChangeUserEmailAsync(Guid userId, string newEmail, string emailChangeToken);
}
public class VerificationService : IVerificationService
{
    private readonly UserManager<User> _userManager;
    private readonly IUserService _userService;
    private readonly ITimeProvider _timeProvider;

    public VerificationService(
        UserManager<User> userManager,
        IUserService userService,
        ITimeProvider timeProvider)
    {
        _userManager = userManager;
        _userService = userService;
        _timeProvider = timeProvider;
    }

    public async Task<string> CreateUserEmailConfirmationTokenAsync(Guid userId)
    {
        User user = await _userService.GetUserByIdAsync(userId);

        return await _userManager.GenerateEmailConfirmationTokenAsync(user);
    }

    public async Task ConfirmUserEmailAsync(string email, string emailConfirmationToken)
    {
        User user = await _userService.GetUserByEmailAsync(email);

        if (user.EmailConfirmed)
        {
            throw new UserEmailAlreadyConfirmedException(user.Id);
        }

        IdentityResult confirmEmailIdentityResult = await _userManager.ConfirmEmailAsync(user, emailConfirmationToken);

        if (!confirmEmailIdentityResult.Succeeded)
        {
            if (confirmEmailIdentityResult.HasInvalidTokenError())
            {
                throw new EmailConfirmationTokenNotValidException();
            }

            throw new IdentityResultException(confirmEmailIdentityResult);
        }

        user.VerifiedDateTimeOffsetUtc = _timeProvider.UtcNow;

        await _userManager.UpdateAsync(user);
    }

    public async Task<string> CreateUserEmailChangeTokenAsync(Guid userId, string newEmail)
    {
        User user = await _userService.GetUserByIdAsync(userId);

        return await _userManager.GenerateChangeEmailTokenAsync(user, newEmail);
    }

    public async Task ChangeUserEmailAsync(Guid userId, string newEmail, string emailChangeToken)
    {
        User user = await _userService.GetUserByIdAsync(userId);

        IdentityResult changeEmailIdentityResult = await _userManager.ChangeEmailAsync(user, newEmail, emailChangeToken);

        if (!changeEmailIdentityResult.Succeeded)
        {
            if (changeEmailIdentityResult.HasInvalidTokenError())
            {
                throw new EmailChangeTokenNotValidException();
            }

            throw new IdentityResultException(changeEmailIdentityResult);
        }
    }
}
