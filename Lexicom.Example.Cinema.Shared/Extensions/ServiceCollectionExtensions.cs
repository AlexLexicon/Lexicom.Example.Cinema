using Lexicom.DependencyInjection.Amenities.Extensions;
using Lexicom.Example.Cinema.Shared.Options;
using Lexicom.Example.Cinema.Shared.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Example.Cinema.Shared.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddShared(this IServiceCollection services)
    {
        services
            .AddOptions<DataOptions>()
            .BindConfiguration();

        services.AddSingleton<IDataService, DataService>();
    }
}
