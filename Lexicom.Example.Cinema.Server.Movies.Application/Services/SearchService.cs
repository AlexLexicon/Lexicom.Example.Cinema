using Lexicom.EntityFramework.Amenities;
using Lexicom.Example.Cinema.Server.Movies.Application.Database;
using Lexicom.Example.Cinema.Server.Movies.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Movies.Application.Services;
public interface ISearchService
{
    Task<Slice<Movie>> SearchMoviesAsync(int pageIndex, int moviesPerPage, string? titleSearchText);
}
public class SearchService : ISearchService
{
    private readonly IDbContextFactory<MoviesDbContext> _dbContextFactory;

    public SearchService(IDbContextFactory<MoviesDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task<Slice<Movie>> SearchMoviesAsync(int pageIndex, int moviesPerPage, string? searchText)
    {
        using var db = await _dbContextFactory.CreateDbContextAsync();

        int totalMovies = await db.Movies.CountAsync();

        IQueryable<Movie> query = db.Movies;

        if (!string.IsNullOrWhiteSpace(searchText))
        {
            string searchTextLower = searchText.ToLower();

            query = query.Where(m => m.Title.ToLower().Contains(searchTextLower));
        }

        List<Movie> movies = await query
            .OrderBy(m => m.Title)
            .Skip(pageIndex * moviesPerPage)
            .Take(moviesPerPage)
            .ToListAsync();

        return new Slice<Movie>(totalMovies, movies);
    }
}
