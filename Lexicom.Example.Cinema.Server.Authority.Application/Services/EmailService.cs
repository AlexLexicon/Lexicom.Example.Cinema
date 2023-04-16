using Lexicom.Cryptography;
using Lexicom.EntityFramework.Identity.Options;
using Lexicom.EntityFramework.Identity.Validators;
using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.Application.Options;
using Lexicom.Example.Cinema.Server.Authority.Application.Validators;
using Lexicom.Extensions.TimeSpans;
using Lexicom.Http;
using Lexicom.Smtp;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Services;
public interface IEmailService
{
    /// <exception cref="UserDoesNotExistException"/>
    Task SendForgotPasswordEmailAsync(Guid userId, string passwordResetToken);
    /// <exception cref="UserDoesNotExistException"/>
    Task SendConfirmationEmailAsync(Guid userId, string emailConfirmationToken);
    /// <exception cref="UserDoesNotExistException"/>
    Task SendChangeEmailAsync(Guid userId, string emailChangeToken);
}
public class EmailService : IEmailService
{
    private readonly ILogger<EmailService> _logger;
    private readonly IUserService _userService;
    private readonly ICryptographyService _cryptographyService;
    private readonly IOptions<BrandOptions> _brandOptions;
    private readonly IOptions<UrlsOptions> _urlsOptions;
    private readonly IOptions<PasswordResetTokenProviderOptions> _passwordResetTokenProviderOptions;
    private readonly IOptions<ChangeEmailTokenProviderOptions> _changeEmailTokenProviderOptions;
    private readonly ISmtpEmailHandler _smtpEmailHandler;

    public EmailService(
        ILogger<EmailService> logger,
        IUserService userService,
        ICryptographyService cryptographyService,
        IOptions<BrandOptions> brandOptions,
        IOptions<UrlsOptions> urlsOptions,
        IOptions<PasswordResetTokenProviderOptions> passwordResetTokenProviderOptions,
        IOptions<ChangeEmailTokenProviderOptions> changeEmailTokenProviderOptions,
        ISmtpEmailHandler smtpEmailHandler)
    {
        _logger = logger;
        _userService = userService;
        _cryptographyService = cryptographyService;
        _brandOptions = brandOptions;
        _urlsOptions = urlsOptions;
        _passwordResetTokenProviderOptions = passwordResetTokenProviderOptions;
        _changeEmailTokenProviderOptions = changeEmailTokenProviderOptions;
        _smtpEmailHandler = smtpEmailHandler;
    }

    public async Task SendForgotPasswordEmailAsync(Guid userId, string passwordResetToken)
    {
        BrandOptions brandOptions = _brandOptions.Value;

        string? companyName = brandOptions.CompanyName;
        string? appName = brandOptions.AppName;

        if (string.IsNullOrWhiteSpace(companyName) || string.IsNullOrWhiteSpace(appName))
        {
            throw BrandOptionsValidator.ToUnreachableException();
        }

        UrlsOptions urlsOptions = _urlsOptions.Value;

        string? url = urlsOptions.ForgotPasswordEmailUrl;

        if (string.IsNullOrWhiteSpace(url))
        {
            throw UrlsOptionsValidator.ToUnreachableException();
        }

        TimeSpan passwordResetTimeSpan = _passwordResetTokenProviderOptions.Value.TokenLifespan;

        if (passwordResetTimeSpan <= TimeSpan.Zero)
        {
            throw IdentityPasswordResetTokenProviderOptionsValidator.ToUnreachableException();
        }

        string passwordResetTimeSpanFriendlyString = passwordResetTimeSpan.ToShortestString();

        User user = await _userService.GetUserByIdAsync(userId);

        var queryString = new HttpQueryString
        {
            new HttpQueryParameter("e", user.Email),
            new HttpQueryParameter("prt", passwordResetToken)
        };

        var firstNameEncryptedBase64DecryptTask = _cryptographyService.DecryptAsync(user.FirstNameEncryptedBase64);
        var lastNameEncryptedBase64DecryptTask = _cryptographyService.DecryptAsync(user.LastNameEncryptedBase64);

        string firstName = await firstNameEncryptedBase64DecryptTask;
        string lastName = await lastNameEncryptedBase64DecryptTask;

        string subject = $"Requested password reset for {appName}";
        string body =
        $"""
        Hello {firstName} {lastName},
        <br>
        <br>
        To reset your {appName} password click on the following link. Please note that this password reset link will expire in {passwordResetTimeSpanFriendlyString}.
        <br>
        <br>
        {url}?{queryString}
        <br>
        <br>
        If you did not request this change, you can safely ignore this email.
        <br>
        <br>
        The {appName} team,
        <br>
        {companyName}
        """;

        await _smtpEmailHandler.SendEmailAsync(user.Email, subject, body);

        _logger.LogInformation("Sent forgot password email to '{userEmail}'.", user.Email);
    }

    public async Task SendConfirmationEmailAsync(Guid userId, string emailConfirmationToken)
    {
        BrandOptions brandOptions = _brandOptions.Value;

        string? companyName = brandOptions.CompanyName;
        string? appName = brandOptions.AppName;

        if (string.IsNullOrWhiteSpace(companyName) || string.IsNullOrWhiteSpace(appName))
        {
            throw BrandOptionsValidator.ToUnreachableException();
        }

        UrlsOptions UrlsOptions = _urlsOptions.Value;

        string? url = UrlsOptions.ConfirmationEmailUrl;

        if (string.IsNullOrWhiteSpace(url))
        {
            throw UrlsOptionsValidator.ToUnreachableException();
        }

        User user = await _userService.GetUserByIdAsync(userId);

        var queryString = new HttpQueryString
        {
            new HttpQueryParameter("e", user.Email),
            new HttpQueryParameter("ct", emailConfirmationToken)
        };

        var firstNameEncryptedBase64DecryptTask = _cryptographyService.DecryptAsync(user.FirstNameEncryptedBase64);
        var lastNameEncryptedBase64DecryptTask = _cryptographyService.DecryptAsync(user.LastNameEncryptedBase64);

        string firstName = await firstNameEncryptedBase64DecryptTask;
        string lastName = await lastNameEncryptedBase64DecryptTask;

        string subject = $"Confirm your email for {appName}";
        string body =
        $"""
        Hello {firstName} {lastName},
        <br>
        <br>
        Welcome to {appName}!
        <br>
        <br>
        All that's left to do is click the link below to confirm your email address so we can finish creating your account.
        <br>
        <br>
        {url}?{queryString}
        <br>
        <br>
        If you did not sign up for {appName}, you should not click the link to confirm this account.
        <br>
        <br>
        The {appName} team,
        <br>
        {companyName}
        """;

        await _smtpEmailHandler.SendEmailAsync(user.Email, subject, body);

        _logger.LogInformation("Sent confirmation email to '{userEmail}'.", user.Email);
    }

    public async Task SendChangeEmailAsync(Guid userId, string emailChangeToken)
    {
        BrandOptions brandOptions = _brandOptions.Value;

        string? companyName = brandOptions.CompanyName;
        string? appName = brandOptions.AppName;

        if (string.IsNullOrWhiteSpace(companyName) || string.IsNullOrWhiteSpace(appName))
        {
            throw BrandOptionsValidator.ToUnreachableException();
        }

        UrlsOptions UrlsOptions = _urlsOptions.Value;

        string? url = UrlsOptions.ConfirmationEmailUrl;

        if (string.IsNullOrWhiteSpace(url))
        {
            throw UrlsOptionsValidator.ToUnreachableException();
        }

        TimeSpan emailChangeTimeSpan = _changeEmailTokenProviderOptions.Value.TokenLifespan;

        if (emailChangeTimeSpan <= TimeSpan.Zero)
        {
            throw IdentityPasswordResetTokenProviderOptionsValidator.ToUnreachableException();
        }

        string emailChangeTimeSpanFriendlyString = emailChangeTimeSpan.ToShortestString();

        User user = await _userService.GetUserByIdAsync(userId);

        var queryString = new HttpQueryString
        {
            new HttpQueryParameter("e", user.Email),
            new HttpQueryParameter("ct", emailChangeToken)
        };

        var firstNameEncryptedBase64DecryptTask = _cryptographyService.DecryptAsync(user.FirstNameEncryptedBase64);
        var lastNameEncryptedBase64DecryptTask = _cryptographyService.DecryptAsync(user.LastNameEncryptedBase64);

        string firstName = await firstNameEncryptedBase64DecryptTask;
        string lastName = await lastNameEncryptedBase64DecryptTask;

        string subject = $"Change your email for {appName}";
        string body =
        $"""
        Hello {firstName} {lastName},
        <br>
        <br>
        To change your {appName} account email click on the following link. Please note that this change email link will expire in {emailChangeTimeSpanFriendlyString}.
        <br>
        <br>
        {url}?{queryString}
        <br>
        <br>
        If you did not request this change, you can safely ignore this email.
        <br>
        <br>
        The {appName} team,
        <br>
        {companyName}
        """;

        await _smtpEmailHandler.SendEmailAsync(user.Email, subject, body);

        _logger.LogInformation("Sent change email to '{userEmail}'.", user.Email);
    }
}
