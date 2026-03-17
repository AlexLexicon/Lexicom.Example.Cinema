using Lexicom.AspNetCore.Controllers.Extensions;
using Lexicom.ConsoleApp.DependencyInjection;
using Lexicom.ConsoleApp.Tui.Extensions;
using Lexicom.DependencyInjection.Primitives.Extensions;
using Lexicom.DependencyInjection.Primitives.For.ConsoleApp.Extensions;
using Lexicom.Example.Cinema.Server.Movies.Application.Database;
using Lexicom.Example.Cinema.Server.Movies.Application.Extensions;
using Lexicom.Example.Cinema.Server.Movies.ConsoleApp;
using Lexicom.Example.Cinema.Shared.Extensions;
using Lexicom.Logging.ConsoleApp.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/*
 * Lexicom.Example.Cinema.Server.Movies.ConsoleApp
 */

ConsoleApplicationBuilder builder = ConsoleApplication.CreateBuilder();

builder.Configuration.AddJsonFile("appsettings.SecretsExample.json");

builder.Lexicom(options =>
{
    options.AddLogging();
    options.AddTui<AssemblyScanMarker>();
    options.AddPrimitives(options =>
    {
        options.AddTimeProvider();
        options.AddGuidProvider();
    });
});

builder.Services.AddDbContextFactory<MoviesDbContext>(options =>
{
    string? sqliteConnectionString = builder.Configuration.GetConnectionString("moviesdb-sqlite");
    string? sqlConnectionString = builder.Configuration.GetConnectionString("moviesdb-sql");

    options.UseSqlite(sqliteConnectionString);
    //options.UseSqlServer(sqlConnectionString);
});

builder.Services.AddShared();
builder.Services.AddMoviesApplication();

ConsoleApplication app = builder.Build();

await app.RunLexicomTuiAsync();
