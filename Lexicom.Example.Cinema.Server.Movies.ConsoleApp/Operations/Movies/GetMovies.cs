using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Movies.Database;
using Lexicom.Example.Cinema.Server.Movies.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Movies.ConsoleApp.Operations.Movies;

[TuiPage("Movies")]
public class GetMovies : ITuiOperation
{
    private readonly IDbContextFactory<MoviesDbContext> _dbContextFactory;

    public GetMovies(IDbContextFactory<MoviesDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task ExecuteAsync()
    {
        using var db = await _dbContextFactory.CreateDbContextAsync();

        List<Movie> movies = await db.Movies.ToListAsync();
        Console.WriteLine("Movies:");
        Consolex.WriteAsJson(movies);
    }
}