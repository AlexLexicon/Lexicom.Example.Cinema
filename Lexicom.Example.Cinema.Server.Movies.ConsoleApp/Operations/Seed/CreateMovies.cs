using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Movies.Application.Services;
using Lexicom.Example.Cinema.Shared.Models;
using Lexicom.Example.Cinema.Shared.Services;

namespace Lexicom.Example.Cinema.Server.Movies.ConsoleApp.Operations.Seed;

[TuiPage("seed")]
public class CreateMovies : ITuiOperation
{
    private readonly IMovieService _movieService;
    private readonly IDataService _dataService;

    public CreateMovies(
        IMovieService movieService,
        IDataService dataService)
    {
        _movieService = movieService;
        _dataService = dataService;
    }

    public async Task ExecuteAsync()
    {
        IReadOnlyList<MovieData>? movies = await _dataService.GetAllMovieDataAsync();

        if (movies is not null)
        {
            foreach (var movie in movies)
            {
                await _movieService.CreateMovieAsync(movie.Title, movie.Duration, movie.ReleaseDateTimeOffsetUtc, movie.Synopsis);
            }

            Console.WriteLine($"Added Movies");
            Consolex.WriteAsJson(movies);
        }
        else
        {
            Console.WriteLine($"No movies found in the json file.");
        }
    }
}
