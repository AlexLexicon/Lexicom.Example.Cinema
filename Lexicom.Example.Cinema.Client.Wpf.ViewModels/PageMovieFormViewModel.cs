using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Lexicom.Example.Cinema.Client.Wpf.ViewModels.Messages;
using Lexicom.Example.Cinema.Server.Movies.Api.Contracts.RuleSets;
using Lexicom.Example.Cinema.Server.Movies.Api.Contracts.RuleSetTransformers;
using Lexicom.Mvvm;
using Lexicom.Validation;

namespace Lexicom.Example.Cinema.Client.Wpf.ViewModels;

public partial class PageMovieFormViewModel : DisposableObservableObject, IAsyncRecipient<ShowPageMovieFormViewMessage>
{
    public PageMovieFormViewModel(
        IRuleSetValidator<MovieTitleStringRuleSet, string?> titleValidator,
        IRuleSetValidator<MovieReleaseStringRuleSet, string?, MovieReleaseStringToDataTimeOffsetTransformer, DateTimeOffset> releaseDateValidator,
        IRuleSetValidator<MovieDurationStringRuleSet, string?, MovieDurationStringToTimeSpanTransformer, TimeSpan> durationValidator,
        IRuleSetValidator<MovieSynopsisStringRuleSet, string?> synopsisValidator)
    {
        TitleValidator = titleValidator;
        ReleaseDateValidator = releaseDateValidator;
        DurationValidator = durationValidator;
        SynopsisValidator = synopsisValidator;
    }

    [ObservableProperty]
    public partial bool IsVisible { get; set; }
    [ObservableProperty]
    public partial bool IsEditing { get; set; }
    [ObservableProperty]
    public partial bool IsValid { get; set; }
    [ObservableProperty]
    public partial IRuleSetValidator<MovieTitleStringRuleSet, string?> TitleValidator { get; set; }
    [ObservableProperty]
    public partial IRuleSetValidator<MovieReleaseStringRuleSet, string?, MovieReleaseStringToDataTimeOffsetTransformer, DateTimeOffset> ReleaseDateValidator { get; set; }
    [ObservableProperty]
    public partial IRuleSetValidator<MovieDurationStringRuleSet, string?, MovieDurationStringToTimeSpanTransformer, TimeSpan> DurationValidator { get; set; }
    [ObservableProperty]
    public partial IRuleSetValidator<MovieSynopsisStringRuleSet, string?> SynopsisValidator { get; set; }

    public Task ReceiveAsync(ShowPageMovieFormViewMessage message, CancellationToken cancellationToken)
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
