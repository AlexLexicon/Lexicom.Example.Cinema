using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using Lexicom.Mvvm;
using Lexicom.Mvvm.Extensions;
using Lexicom.Validation;
using Lexicom.Validation.Amenities.RuleSets;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;

public partial class SignInViewModel : DisposableObservableObject, IAsyncRecipient<SignInDialogShowMessage>, IAsyncRecipient<SignInSuccessNotification>, IAsyncRecipient<SignInFailedNotification>
{
    private readonly IMessenger _messenger;

    public SignInViewModel(
        IMessenger messenger,
        IRuleSetValidator<EmailRuleSet, string?> emailValidator,
        IRuleSetValidator<RequiredRuleSet, string?> requiredValidator)
    {
        _messenger = messenger;
        EmailValidator = emailValidator;
        PasswordValidator = requiredValidator;
    }

    [ObservableProperty]
    public partial bool IsVisible { get; set; }
    [ObservableProperty]
    public partial string? Email { get; set; }
    [ObservableProperty]
    public partial IRuleSetValidator<EmailRuleSet, string?> EmailValidator { get; set; }
    [ObservableProperty]
    public partial string? Password { get; set; }
    [ObservableProperty]
    public partial IRuleSetValidator<RequiredRuleSet, string?> PasswordValidator { get; set; }
    [ObservableProperty]
    public partial bool IsValid { get; set; }

    public Task ReceiveAsync(SignInDialogShowMessage message, CancellationToken cancellationToken)
    {
        IsVisible = true;

        return Task.CompletedTask;
    }

    public Task ReceiveAsync(SignInSuccessNotification message, CancellationToken cancellationToken)
    {
        IsVisible = false;

        return Task.CompletedTask;
    }

    public Task ReceiveAsync(SignInFailedNotification message, CancellationToken cancellationToken)
    {
        if (message.Error is SignInFailedNotification.Errors.IncorrectCredentials)
        {
            EmailValidator.ValidationErrors.Add("Wrong email or password");
            PasswordValidator.ValidationErrors.Add("Wrong email or password");
        }
        else if (message.Error is SignInFailedNotification.Errors.LockedOut)
        {
            EmailValidator.ValidationErrors.Add("Locked out");
            PasswordValidator.ValidationErrors.Add("Locked out");
        }

        return Task.CompletedTask;
    }

    [RelayCommand]
    private void Hide()
    {
        IsVisible = false;
    }

    [RelayCommand]
    private void Validation()
    {
        IsValid = EmailValidator.IsValid && PasswordValidator.IsValid;
    }

    [RelayCommand]
    private async Task SignInAsync()
    {
        if (IsValid && Email is not null && Password is not null)
        {
            await _messenger.SendAsync(new SignInNotification(Email, Password));
        }
    }
}
