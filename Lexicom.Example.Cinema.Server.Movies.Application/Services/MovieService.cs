using Lexicom.DependencyInjection.Primitives;
using Lexicom.Example.Cinema.Server.Movies.Application.Database;
using Lexicom.Example.Cinema.Server.Movies.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Movies.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Movies.Application.Services;
public interface IMovieService
{
    /// <exception cref="MovieDoesNotExistException"/>
    Task<Movie> GetMovieAsync(Guid movieId);
    /// <exception cref="TitleAlreadyInUseException"/>
    Task<Movie> CreateMovieAsync(string title, TimeSpan duration, DateTimeOffset releaseDateTimeOffsetUtc, string synopsis);
    /// <exception cref="MovieDoesNotExistException"/>
    /// <exception cref="TitleAlreadyInUseException"/>
    Task UpdateMovieAsync(Guid movieId, string? newTitle, TimeSpan? newDuration, DateTimeOffset? newReleaseDateTimeOffsetUtc, string? newSynopsis);
}
public class MovieService : IMovieService
{
    private readonly IDbContextFactory<MoviesDbContext> _dbContextFactory;
    private readonly IGuidProvider _guidProvider;
    private readonly ITimeProvider _timeProvider;

    public MovieService(
        IDbContextFactory<MoviesDbContext> dbContextFactory,
        IGuidProvider guidProvider,
        ITimeProvider timeProvider)
    {
        _dbContextFactory = dbContextFactory;
        _guidProvider = guidProvider;
        _timeProvider = timeProvider;
    }

    public async Task<Movie> GetMovieAsync(Guid movieId)
    {
        using var db = await _dbContextFactory.CreateDbContextAsync();

        Movie? movie = await db.Movies.SingleOrDefaultAsync(m => m.Id == movieId);

        if (movie is null)
        {
            throw new MovieDoesNotExistException(movieId);
        }

        return movie;
    }

    public async Task<bool> MovieExistsAsync(string title)
    {
        using var db = await _dbContextFactory.CreateDbContextAsync();

        return await db.Movies.AnyAsync(m => m.Title.ToLower() == title.ToLower());
    }

    public async Task<Movie> CreateMovieAsync(string title, TimeSpan duration, DateTimeOffset releaseDateTimeOffsetUtc, string synopsis)
    {
        using var db = await _dbContextFactory.CreateDbContextAsync();

        bool titleExists = await MovieExistsAsync(title);

        if (titleExists)
        {
            throw new TitleAlreadyInUseException(title);
        }

        var movie = new Movie
        {
            Id = _guidProvider.NewGuid(),
            Title = title,
            Duration = duration,
            ReleaseDateTimeOffsetUtc = releaseDateTimeOffsetUtc,
            Synopsis = synopsis,
            CreatedDateTimeOffsetUtc = _timeProvider.UtcNow,
            ModifiedDateTimeOffsetUtc = _timeProvider.UtcNow,
        };

        await db.AddAsync(movie);

        await db.SaveChangesAsync();

        return movie;
    }

    public async Task UpdateMovieAsync(Guid movieId, string? newTitle, TimeSpan? newDuration, DateTimeOffset? newReleaseDateTimeOffsetUtc, string? newSynopsis)
    {
        bool isModified = false;

        Movie movie = await GetMovieAsync(movieId);

        using var db = await _dbContextFactory.CreateDbContextAsync();

        if (newTitle is not null)
        {
            bool titleExists = await MovieExistsAsync(newTitle);

            if (titleExists)
            {
                throw new TitleAlreadyInUseException(newTitle);
            }

            movie.Title = newTitle;
            isModified = true;
        }

        if (newDuration is not null)
        {
            movie.Duration = newDuration.Value;
            isModified = true;
        }

        if (newReleaseDateTimeOffsetUtc is not null)
        {
            movie.ReleaseDateTimeOffsetUtc = newReleaseDateTimeOffsetUtc.Value;
            isModified = true;
        }

        if (newSynopsis is not null)
        {
            movie.Synopsis = newSynopsis;
            isModified = true;
        }

        if (isModified)
        {
            movie.ModifiedDateTimeOffsetUtc = _timeProvider.UtcNow;

            db.Movies.Update(movie);

            await db.SaveChangesAsync();
        }
    }
}
