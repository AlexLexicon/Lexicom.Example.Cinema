using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Services;
public interface ICommunicationService
{
    /// <exception cref="UserDoesNotExistException"/>
    Task AssembleAndSendUserForgotPasswordEmailAsync(string email);
    /// <exception cref="UserDoesNotExistException"/>
    Task AssembleAndSendUserConfirmEmailCommunciationAsync(string email);
    /// <exception cref="UserDoesNotExistException"/>
    Task AssembleAndSendUserConfirmEmailCommunciationAsync(Guid userId);
    /// <exception cref="UserDoesNotExistException"/>
    Task AssembleAndSendChangeEmailCommunicationAsync(Guid userId, string newEmail);
}
public class CommunicationService : ICommunicationService
{
    private readonly IUserService _userService;
    private readonly IVerificationService _verificationService;
    private readonly IEmailService _emailService;
    private readonly IPasswordService _passwordService;

    public CommunicationService(
        IUserService userService,
        IVerificationService verificationService,
        IEmailService emailService,
        IPasswordService passwordService)
    {
        _userService = userService;
        _verificationService = verificationService;
        _emailService = emailService;
        _passwordService = passwordService;
    }

    public async Task AssembleAndSendUserForgotPasswordEmailAsync(string email)
    {
        User user = await _userService.GetUserByEmailAsync(email);

        string passwordResetToken;
        try
        {
            passwordResetToken = await _passwordService.CreateUserPasswordResetTokenAsync(user.Id);
        }
        catch (UserDoesNotExistException e)
        {
            throw e.ToUnreachableException();
        }

        try
        {
            await _emailService.SendForgotPasswordEmailAsync(user.Id, passwordResetToken);
        }
        catch (UserDoesNotExistException e)
        {
            throw e.ToUnreachableException();
        }
    }

    public async Task AssembleAndSendUserConfirmEmailCommunciationAsync(string email)
    {
        User user = await _userService.GetUserByEmailAsync(email);

        await SendUserConfirmEmailCommunciationAsync(user);
    }
    public async Task AssembleAndSendUserConfirmEmailCommunciationAsync(Guid userId)
    {
        User user = await _userService.GetUserByIdAsync(userId);

        await SendUserConfirmEmailCommunciationAsync(user);
    }
    private async Task SendUserConfirmEmailCommunciationAsync(User user)
    {
        string emailConfirmationToken;
        try
        {
            emailConfirmationToken = await _verificationService.CreateUserEmailConfirmationTokenAsync(user.Id);
        }
        catch (UserDoesNotExistException e)
        {
            throw e.ToUnreachableException();
        }

        try
        {
            await _emailService.SendConfirmationEmailAsync(user.Id, emailConfirmationToken);
        }
        catch (UserDoesNotExistException e)
        {
            throw e.ToUnreachableException();
        }
    }

    public async Task AssembleAndSendChangeEmailCommunicationAsync(Guid userId, string newEmail)
    {
        User user = await _userService.GetUserByIdAsync(userId);

        string emailChangeToken;
        try
        {
            emailChangeToken = await _verificationService.CreateUserEmailChangeTokenAsync(user.Id, newEmail);
        }
        catch (UserDoesNotExistException e)
        {
            throw e.ToUnreachableException();
        }

        try
        {
            await _emailService.SendChangeEmailAsync(user.Id, emailChangeToken);
        }
        catch (UserDoesNotExistException e)
        {
            throw e.ToUnreachableException();
        }
    }
}
