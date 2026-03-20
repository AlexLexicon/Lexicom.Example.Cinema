using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Example.Cinema.Server.Movies.Database.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddMoviesDatabase(this IServiceCollection services)
    {
        services.AddDbContextFactory<MoviesDbContext>((sp, options) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            string? sqliteConnectionString = configuration.GetConnectionString("moviesdb-sqlite");
            string? sqlConnectionString = configuration.GetConnectionString("moviesdb-sql");

            options.UseSqlite(sqliteConnectionString);
            //options.UseSqlServer(sqlConnectionString);
        });
    }
}
