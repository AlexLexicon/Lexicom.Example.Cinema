using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
using Lexicom.Example.Cinema.Server.Movies.Api.Contracts.RuleSets;
using Lexicom.Example.Cinema.Server.Movies.Api.Contracts.RuleSetTransformers;
using Lexicom.Validation;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;

public partial class PageMovieFormViewModel : ObservableObject, INotificationHandler<ShowPageMovieFormViewNotification>
{
    public PageMovieFormViewModel(
        IRuleSetValidator<MovieTitleStringRuleSet, string?> titleValidator,
        IRuleSetValidator<MovieReleaseStringRuleSet, string?, MovieReleaseStringToDataTimeOffsetTransformer, DateTimeOffset> releaseDateValidator,
        IRuleSetValidator<MovieDurationStringRuleSet, string?, MovieDurationStringToTimeSpanTransformer, TimeSpan> durationValidator,
        IRuleSetValidator<MovieSynopsisStringRuleSet, string?> synopsisValidator)
    {
        _titleValidator = titleValidator;
        _releaseDateValidator = releaseDateValidator;
        _durationValidator = durationValidator;
        _synopsisValidator = synopsisValidator;
    }

    [ObservableProperty]
    private bool _isVisible;
    [ObservableProperty]
    private bool _isEditing;
    [ObservableProperty]
    private bool _isValid;
    [ObservableProperty]
    private IRuleSetValidator<MovieTitleStringRuleSet, string?> _titleValidator;
    [ObservableProperty]
    private IRuleSetValidator<MovieReleaseStringRuleSet, string?, MovieReleaseStringToDataTimeOffsetTransformer, DateTimeOffset> _releaseDateValidator;
    [ObservableProperty]
    private IRuleSetValidator<MovieDurationStringRuleSet, string?, MovieDurationStringToTimeSpanTransformer, TimeSpan> _durationValidator;
    [ObservableProperty]
    private IRuleSetValidator<MovieSynopsisStringRuleSet, string?> _synopsisValidator;

    public Task Handle(ShowPageMovieFormViewNotification notification, CancellationToken cancellationToken)
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
        IsValid = TitleValidator.IsValid && ReleaseDateValidator.IsValid && DurationValidator.IsValid && SynopsisValidator.IsValid;
    }
}
