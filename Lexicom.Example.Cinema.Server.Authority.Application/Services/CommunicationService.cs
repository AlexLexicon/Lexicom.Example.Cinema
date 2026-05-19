using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Database.Entities;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Services;

public interface ICommunicationService
{
    /// <exception cref="UserDoesNotExistException"/>
    Task AssembleAndSendUserForgotPasswordEmailAsync(string email);
    /// <exception cref="UserDoesNotExistException"/>
    Task AssembleAndSendUserConfirmEmailCommunicationAsync(string email);
    /// <exception cref="UserDoesNotExistException"/>
    Task AssembleAndSendUserConfirmEmailCommunicationAsync(Guid userId);
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

    public async Task AssembleAndSendUserConfirmEmailCommunicationAsync(string email)
    {
        User user = await _userService.GetUserByEmailAsync(email);

        await SendUserConfirmEmailCommunicationAsync(user);
    }
    public async Task AssembleAndSendUserConfirmEmailCommunicationAsync(Guid userId)
    {
        User user = await _userService.GetUserByIdAsync(userId);

        await SendUserConfirmEmailCommunicationAsync(user);
    }
    private async Task SendUserConfirmEmailCommunicationAsync(User user)
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
