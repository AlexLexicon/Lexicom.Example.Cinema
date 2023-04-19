using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
using Lexicom.Validation;
using Lexicom.Validation.Amenities.RuleSets;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class SignInViewModel : ObservableObject, INotificationHandler<ShowSignInViewNotification>, INotificationHandler<SignInSuccessNotification>, INotificationHandler<SignInFailedNotification>
{
    private readonly IMediator _mediator;

    public SignInViewModel(
        IMediator mediator,
        IRuleSetValidator<EmailRuleSet, string?> emailValidator,
        IRuleSetValidator<RequiredRuleSet, string?> requiredValidator)
    {
        _mediator = mediator;
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

    public Task Handle(ShowSignInViewNotification notification, CancellationToken cancellationToken)
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
            EmailValidator.SetInvalid("Wrong email or password");
            PasswordValidator.SetInvalid("Wrong email or password");
        }
        else if (notification.Error is SignInFailedNotification.Errors.LockedOut)
        {
            EmailValidator.SetInvalid("Locked out");
            PasswordValidator.SetInvalid("Locked out");
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
            await _mediator.Publish(new SignInNotification(Email, Password));
        }
    }
}
