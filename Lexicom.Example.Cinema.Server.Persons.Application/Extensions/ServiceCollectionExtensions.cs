using Lexicom.Example.Cinema.Server.Persons.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Example.Cinema.Server.Persons.Application.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddPersonsApplication(this IServiceCollection services)
    {
        services.AddScoped<ISearchService, SearchService>();
    }
}
