using Lexicom.Example.Cinema.Server.Movies.Api.Contracts.Movie;

namespace Lexicom.Example.Cinema.Client.Application.Services;

public interface IMovieService
{
    Task<MovieGetResponseBody> GetMovieAsync(Guid movieId);
}
public class MovieService : IMovieService
{
    public MovieService()
    {
        Movies = [];
    }

    private Dictionary<Guid, MovieGetResponseBody> Movies { get; }

    public Task<MovieGetResponseBody> GetMovieAsync(Guid movieId)
    {
        if (!Movies.TryGetValue(movieId, out MovieGetResponseBody? movie))
        {
            throw new Exception($"Failed to get the movie with the id '{movieId}'.");
        }

        return Task.FromResult(movie);
    }
}
