using Lexicom.AspNetCore.Controllers.Extensions;
using Lexicom.Authority.ConsoleApp.Extensions;
using Lexicom.ConsoleApp.DependencyInjection;
using Lexicom.ConsoleApp.Tui.Extensions;
using Lexicom.Cryptography.ConsoleApp.Extensions;
using Lexicom.Cryptography.Extensions;
using Lexicom.DependencyInjection.Primitives.Extensions;
using Lexicom.DependencyInjection.Primitives.For.ConsoleApp.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Application.Extensions;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;
using Lexicom.Example.Cinema.Server.Authority.Database.Extensions;
using Lexicom.Logging.ConsoleApp.Extensions;
using Lexicom.Smtp.ConsoleApp.Extensions;
using Lexicom.Smtp.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/*
 * Lexicom.Example.Cinema.Server.Authority.ConsoleApp
 */

ConsoleApplicationBuilder builder = ConsoleApplication.CreateBuilder();

builder.Configuration.AddJsonFile("appsettings.SecretsExample.json");

builder.Lexicom(options =>
{
    options.AddLogging();
    options.AddTui<AssemblyScanMarker>();
    options.AddAuthority();
    options.AddSmtp(options =>
    {
        options.AddFileClient();
    });
    options.AddCryptography(options =>
    {
        options.AddStringSecretOptions();
    });
    options.AddPrimitives(options =>
    {
        options.AddTimeProvider();
        options.AddGuidProvider();
    });
});

builder.Services.AddScoped<IComprehensiveService, ComprehensiveService>();

builder.Services.AddAuthorityDatabase();
builder.Services.AddAuthorityApplication();

ConsoleApplication app = builder.Build();

await app.RunLexicomTuiAsync();
