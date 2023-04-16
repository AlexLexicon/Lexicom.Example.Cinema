using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Temp;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Application.Handlers;
public class MovieGetHandler : IRequestHandler<MovieGetRequest, MovieGetResponse>
{
    private readonly IDomainsStore _moviesStore;

    public MovieGetHandler(IDomainsStore moviesStore)
    {
        _moviesStore = moviesStore;
    }

    public async Task<MovieGetResponse> Handle(MovieGetRequest request, CancellationToken cancellationToken)
    {
        var movie = _moviesStore.Movies.First(m => m.Id == request.MovieId);

        await Task.Delay(500, cancellationToken);

        return new MovieGetResponse(movie.Title, movie.Duration, movie.ReleaseDateTimeOffsetUtc, movie.Synopsis);
    }
}
