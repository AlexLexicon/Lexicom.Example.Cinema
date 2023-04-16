using Lexicom.Example.Cinema.Server.Movies.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Example.Cinema.Server.Movies.Application.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddMoviesApplication(this IServiceCollection services)
    {
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<ISearchService, SearchService>();
    }
}
