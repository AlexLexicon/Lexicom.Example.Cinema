using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Example.Cinema.Server.Shared.Extensions;

public static class ServiceProviderExtensions
{
    public static async Task EnsureDatabaseCreatedAsync<TDbContext>(this IServiceProvider serviceProvider, CancellationToken cancellationToken = default) where TDbContext : DbContext
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);

        var dbContextFactory = serviceProvider.GetRequiredService<IDbContextFactory<TDbContext>>();

        await using var db = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        await db.Database.EnsureCreatedAsync(cancellationToken);
    }
}
