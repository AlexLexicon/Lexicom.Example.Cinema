using Lexicom.EntityFramework.Identity.Exceptions;
using Lexicom.EntityFramework.Identity.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Microsoft.AspNetCore.Identity;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Services;
public interface IPasswordService
{
    /// <exception cref="UserDoesNotExistException"/>
    /// <exception cref="PasswordMissingRequirementsException"/>
    /// <exception cref="PasswordIncorrectException"/>
    Task ChangeUserPasswordAsync(Guid userId, string currentPassword, string newPassword);
    /// <exception cref="UserDoesNotExistException"/>
    Task<string> CreateUserPasswordResetTokenAsync(Guid userId);
    /// <exception cref="UserDoesNotExistException"/>
    /// <exception cref="PasswordMissingRequirementsException"/>
    /// <exception cref="PasswordResetTokenNotValidException"/>
    Task ResetUserPasswordAsync(string email, string passwordResetToken, string newPassword);
}
public class PasswordService : IPasswordService
{
    private readonly IUserService _userService;
    private readonly UserManager<User> _userManager;

    public PasswordService(
        IUserService userService,
        UserManager<User> userManager)
    {
        _userService = userService;
        _userManager = userManager;
    }

    public async Task ChangeUserPasswordAsync(Guid userId, string currentPassword, string newPassword)
    {
        User user = await _userService.GetUserByIdAsync(userId);

        IdentityResult changePasswordIdentityResult = await _userManager.ChangePasswordAsync(user, currentPassword, newPassword);

        if (!changePasswordIdentityResult.Succeeded)
        {
            if (changePasswordIdentityResult.HasPasswordMissingRequirementsError())
            {
                throw new PasswordMissingRequirementsException();
            }

            if (changePasswordIdentityResult.HasPasswordMismatchError())
            {
                throw new PasswordIncorrectException();
            }

            throw new IdentityResultException(changePasswordIdentityResult);
        }
    }

    public async Task<string> CreateUserPasswordResetTokenAsync(Guid userId)
    {
        User user = await _userService.GetUserByIdAsync(userId);

        return await _userManager.GeneratePasswordResetTokenAsync(user);
    }

    public async Task ResetUserPasswordAsync(string email, string passwordResetToken, string newPassword)
    {
        User user = await _userService.GetUserByEmailAsync(email);

        IdentityResult resetPasswordIdentityResult = await _userManager.ResetPasswordAsync(user, passwordResetToken, newPassword);

        if (!resetPasswordIdentityResult.Succeeded)
        {
            if (resetPasswordIdentityResult.HasPasswordMissingRequirementsError())
            {
                throw new PasswordMissingRequirementsException();
            }

            //this will be the error that occurs when the token has expired.
            if (resetPasswordIdentityResult.HasInvalidTokenError())
            {
                throw new PasswordResetTokenNotValidException();
            }

            throw new IdentityResultException(resetPasswordIdentityResult);
        }
    }
}
