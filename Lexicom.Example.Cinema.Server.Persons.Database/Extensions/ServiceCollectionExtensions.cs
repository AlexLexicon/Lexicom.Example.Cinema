using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Example.Cinema.Server.Persons.Database.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPersonsDatabase(this IServiceCollection services)
    {
        services.AddDbContextFactory<PersonsDbContext>((sp, options) =>
        {
            var configuration = sp.GetRequiredService<IConfiguration>();

            string? sqliteConnectionString = configuration.GetConnectionString("personsdb-sqlite");
            string? sqlConnectionString = configuration.GetConnectionString("personsdb-sql");

            options.UseSqlite(sqliteConnectionString);
            //options.UseSqlServer(sqlConnectionString);
        });
    }
}
