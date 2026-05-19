using Lexicom.ConsoleApp.DependencyInjection;
using Lexicom.ConsoleApp.Tui.Extensions;
using Lexicom.DependencyInjection.Primitives.Extensions;
using Lexicom.DependencyInjection.Primitives.For.ConsoleApp.Extensions;
using Lexicom.Example.Cinema.Server.Persons.Application.Extensions;
using Lexicom.Example.Cinema.Server.Persons.ConsoleApp;
using Lexicom.Example.Cinema.Server.Persons.Database;
using Lexicom.Example.Cinema.Server.Persons.Database.Extensions;
using Lexicom.Example.Cinema.Server.Shared.Extensions;
using Lexicom.Logging.ConsoleApp.Extensions;
using Lexicom.Supports.ConsoleApp.Extensions;
using Microsoft.Extensions.DependencyInjection;

/*
 * Lexicom.Example.Cinema.Server.Persons.ConsoleApp
 */

ConsoleApplicationBuilder builder = ConsoleApplication.CreateBuilder();

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

builder.Services.AddPersonsDatabase();
builder.Services.AddPersonsApplication();

ConsoleApplication app = builder.Build();

await app.Services.EnsureDatabaseCreatedAsync<PersonsDbContext>();

await app.RunLexicomTuiAsync("Lexicom.Example.Cinema.Server.Persons.ConsoleApp");
