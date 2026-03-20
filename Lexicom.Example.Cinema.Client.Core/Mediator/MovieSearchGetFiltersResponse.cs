namespace Lexicom.Example.Cinema.Client.Core.Mediator;
public record class MovieSearchGetFiltersResponse(bool IsTitleFilter, bool IsReleaseDateFilter, bool IsDurationFilter, bool IsSynopsisFilter);