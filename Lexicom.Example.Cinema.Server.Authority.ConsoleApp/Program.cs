using Lexicom.Authentication.For.ConsoleApp.Extensions;
using Lexicom.Authority.ConsoleApp.Extensions;
using Lexicom.Authority.Extensions;
using Lexicom.ConsoleApp.DependencyInjection;
using Lexicom.ConsoleApp.Tui.Extensions;
using Lexicom.Cryptography.ConsoleApp.Extensions;
using Lexicom.Cryptography.Extensions;
using Lexicom.DependencyInjection.Primitives.Extensions;
using Lexicom.DependencyInjection.Primitives.For.ConsoleApp.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Application.Extensions;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;
using Lexicom.Example.Cinema.Server.Authority.Database;
using Lexicom.Example.Cinema.Server.Authority.Database.Extensions;
using Lexicom.Example.Cinema.Server.Shared.Extensions;
using Lexicom.Logging.ConsoleApp.Extensions;
using Lexicom.Smtp.ConsoleApp.Extensions;
using Lexicom.Smtp.Extensions;
using Lexicom.Supports.ConsoleApp.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/*
 * Lexicom.Example.Cinema.Server.Authority.ConsoleApp
 */

ConsoleApplicationBuilder builder = ConsoleApplication.CreateBuilder();

builder.Configuration.AddJsonFile("appsettings.SecretsExample.json");

builder.Lexicom(l =>
{
    l.AddLogging();
    l.AddTui<AssemblyScanMarker>();
    l.AddAuthority(auth =>
    {
        auth.AddAccessTokenProvider();
        auth.AddRefreshTokenProvider();
    });
    l.AddAuthentication(auth =>
    {
        auth.AddAccessTokenAuthentication();
    });
    l.AddSmtp(smtp =>
    {
        smtp.AddFileClient();
    });
    l.AddCryptography(c =>
    {
        c.AddStringSecretOptions();
    });
    l.AddPrimitives(p =>
    {
        p.AddTimeProvider();
        p.AddGuidProvider();
    });
});

builder.Services.AddAuthorityDatabase();
builder.Services.AddAuthorityApplication();

builder.Services.AddScoped<IExtendedComprehensiveService, ExtendedComprehensiveService>();
builder.Services.AddSingleton<IDateTimeService, DateTimeService>();

ConsoleApplication app = builder.Build();

await app.Services.EnsureDatabaseCreatedAsync<AuthorityDbContext>();

await app.RunLexicomTuiAsync("Lexicom.Example.Cinema.Server.Authority.ConsoleApp");
