using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
using Lexicom.Validation;
using Lexicom.Validation.Amenities.RuleSets;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class SignInViewModel : ObservableObject, INotificationHandler<ShowSignInViewNotification>
{

    public SignInViewModel(
        IRuleSetValidator<EmailRuleSet, string?> emailValidator,
        IRuleSetValidator<RequiredRuleSet, string?> requiredValidator)
    {
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
}
