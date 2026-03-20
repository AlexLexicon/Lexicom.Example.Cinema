using Lexicom.EntityFramework.Identity.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Database.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Example.Cinema.Server.Authority.Database.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddAuthorityDatabase(this IServiceCollection services)
    {
        services.AddLexicomEntityFrameworkIdentity<AuthorityDbContext, User, Role, Guid>();
        services.AddDbContextFactory<AuthorityDbContext>((sp, options) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            string? sqliteConnectionString = configuration.GetConnectionString("authoritydb-sqlite");
            string? sqlConnectionString = configuration.GetConnectionString("authoritydb-sql");

            options.UseSqlite(sqliteConnectionString);
            //options.UseSqlServer(sqlConnectionString);
        });
    }
}
