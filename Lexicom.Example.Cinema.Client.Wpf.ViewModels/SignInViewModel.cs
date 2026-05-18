using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
using Lexicom.Mvvm;
using Lexicom.Validation;
using Lexicom.Validation.Amenities.RuleSets;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;

public partial class SignInViewModel : ObservableObject, IAsyncRecipient<ShowSignInViewMessage>, IAsyncRecipient<SignInSuccessNotification>, IAsyncRecipient<SignInFailedNotification>
{
    private readonly IMessenger _messenger;

    public SignInViewModel(
        IMessenger messenger,
        IRuleSetValidator<EmailRuleSet, string?> emailValidator,
        IRuleSetValidator<RequiredRuleSet, string?> requiredValidator)
    {
        _messenger = messenger;
        _emailValidator = emailValidator;
        _passwordValidator = requiredValidator;
    }

    [ObservableProperty]
    private bool _isVisible;
    [ObservableProperty]
    private string? _email;
    [ObservableProperty]
    private IRuleSetValidator<EmailRuleSet, string?> _emailValidator;
    [ObservableProperty]
    private string? _password;
    [ObservableProperty]
    private IRuleSetValidator<RequiredRuleSet, string?> _passwordValidator;
    [ObservableProperty]
    private bool _isValid;

    public Task Handle(ShowSignInViewMessage notification, CancellationToken cancellationToken)
    {
        IsVisible = true;

        return Task.CompletedTask;
    }

    public Task Handle(SignInSuccessNotification notification, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task Handle(SignInFailedNotification notification, CancellationToken cancellationToken)
    {
        if (notification.Error is SignInFailedNotification.Errors.IncorrectCredentials)
        {
            EmailValidator.ValidationErrors.Add("Wrong email or password");
            PasswordValidator.ValidationErrors.Add("Wrong email or password");
        }
        else if (notification.Error is SignInFailedNotification.Errors.LockedOut)
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
            await _messenger.Publish(new SignInNotification(Email, Password));
        }
    }
}
