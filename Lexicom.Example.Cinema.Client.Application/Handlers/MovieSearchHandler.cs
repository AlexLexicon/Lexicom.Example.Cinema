using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Temp;
using Lexicom.Extensions.Expressions;
using MediatR;
using System.Linq.Expressions;

namespace Lexicom.Example.Cinema.Client.Application.Handlers;
public class MovieSearchHandler : INotificationHandler<MovieSearchRequestNotification>
{
    private readonly IMediator _mediator;
    private readonly IDomainsStore _domainsStore;

    public MovieSearchHandler(
        IMediator mediator,
        IDomainsStore domainsStore)
    {
        _mediator = mediator;
        _domainsStore = domainsStore;
    }

    public async Task Handle(MovieSearchRequestNotification notification, CancellationToken cancellationToken)
    {
        var movieSearchGetTextRequestTask = _mediator.Send(new MovieSearchGetTextRequest(), cancellationToken);
        var movieSearchGetFiltersRequestTask = _mediator.Send(new MovieSearchGetFiltersRequest(), cancellationToken);

        MovieSearchGetTextResponse movieSearchGetTextResponse = await movieSearchGetTextRequestTask;
        MovieSearchGetFiltersResponse movieSearchGetFiltersResponse = await movieSearchGetFiltersRequestTask;

        int pageIndex = notification.PageIndex;
        int pageLimit = notification.PageLimit;
        string? searchText = movieSearchGetTextResponse.SearchText?.ToLower();

        var query = _domainsStore.Movies.Where(m => true);

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            Expression<Func<MovieStore, bool>>? where = null;

            if (movieSearchGetFiltersResponse.IsTitleFilter)
            {
                where = where.Or(m => m.Title.ToLower().Contains(searchText));
            }
            if (movieSearchGetFiltersResponse.IsReleaseDateFilter)
            {
                where = where.Or(m => m.ReleaseDateTimeOffsetUtc.ToString("MM/dd/yyyy").ToLower().Contains(searchText));
            }
            if (movieSearchGetFiltersResponse.IsDurationFilter)
            {
                where = where.Or(m => m.Duration.ToString().ToLower().Contains(searchText));
            }
            if (movieSearchGetFiltersResponse.IsSynopsisFilter)
            {
                where = where.Or(m => m.Synopsis.ToLower().Contains(searchText));
            }

            if (where is not null)
            {
                query = query.Where(where.Compile());
            }
        }

        query = query.OrderBy(m => m.Title);

        int totalCount = query.Count();

        List<MovieSearchResponseNotificationMovie> results = query
            .Skip(pageIndex * pageLimit)
            .Take(pageLimit)
            .Select(m => new MovieSearchResponseNotificationMovie(m.Id, m.Title, m.ReleaseDateTimeOffsetUtc, m.Duration, m.Synopsis))
            .ToList();

        await Task.Delay(500, cancellationToken);

        await _mediator.Publish(new MovieSearchResponseNotification(results, totalCount), cancellationToken);
    }
}