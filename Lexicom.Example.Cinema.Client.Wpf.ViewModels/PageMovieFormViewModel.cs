using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Mediator;
using Lexicom.Example.Cinema.Server.Movies.Api.Contracts.RuleSets;
using Lexicom.Validation;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;
public partial class PageMovieFormViewModel : ObservableObject, INotificationHandler<ShowPageMovieFormViewNotification>
{
    public PageMovieFormViewModel(
        IRuleSetValidator<MovieTitleRuleSet, string?> titleValidator,
        IRuleSetValidator<MovieReleaseDateTimeOffsetUtcRuleSet, DateTimeOffset, string?, ReleaseDateTransformer> releaseDateValidator,
        IRuleSetValidator<MovieDurationRuleSet, TimeSpan, string?, DurationTransformer> durationValidator,
        IRuleSetValidator<MovieSynopsisRuleSet, string?> synopsisValidator)
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
    private IRuleSetValidator<MovieTitleRuleSet, string?> _titleValidator;
    [ObservableProperty]
    private IRuleSetValidator<MovieReleaseDateTimeOffsetUtcRuleSet, DateTimeOffset, string?, ReleaseDateTransformer> _releaseDateValidator;
    [ObservableProperty]
    private IRuleSetValidator<MovieDurationRuleSet, TimeSpan, string?, DurationTransformer> _durationValidator;
    [ObservableProperty]
    private IRuleSetValidator<MovieSynopsisRuleSet, string?> _synopsisValidator;

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

    public class ReleaseDateTransformer : IRuleSetTransfromer<DateTimeOffset, string?>
    {
        public bool TryTransform(string? inProperty, out DateTimeOffset property)
        {
            return DateTimeOffset.TryParse(inProperty, out property);
        }

        public string ErrorMessageTypeName => "Date";
    }

    public class DurationTransformer : IRuleSetTransfromer<TimeSpan, string?>
    {
        public bool TryTransform(string? inProperty, out TimeSpan property)
        {
            return TimeSpan.TryParse(inProperty, out property);
        }

        public string ErrorMessageTypeName => "Duration";
    }
}
