using Lexicom.AspNetCore.Controllers.Extensions;
using Lexicom.Authority.ConsoleApp.Extensions;
using Lexicom.ConsoleApp.DependencyInjection;
using Lexicom.ConsoleApp.Tui.Extensions;
using Lexicom.Cryptography.ConsoleApp.Extensions;
using Lexicom.Cryptography.Extensions;
using Lexicom.DependencyInjection.Primitives.For.ConsoleApp.Extensions;
using Lexicom.EntityFramework.Identity.ConsoleApp.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Application.Database;
using Lexicom.Example.Cinema.Server.Authority.Application.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp;
using Lexicom.Logging.ConsoleApp.Extensions;
using Lexicom.Smtp.ConsoleApp.Extensions;
using Lexicom.Smtp.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

/*
 * Lexicom.Example.Cinema.Server.Authority.ConsoleApp
 */

ConsoleApplicationBuilder builder = ConsoleApplication.CreateBuilder();

builder.Configuration.AddJsonFile("appsettings.json");
builder.Configuration.AddJsonFile("appsettings.Example.json");

builder.Lexicom(options =>
{
    options.AddTimeProvider();
    options.AddGuidProvider();
    options.AddLogging();
    options.AddTui<AssemblyScanMarker>();
    options.AddAuthority();
    options.AddEntityFrameworkIdentity<AuthorityDbContext, User, Role, Guid>();
    options.AddSmtp(options =>
    {
        options.AddFileClient();
    });
    options.AddCryptography(options =>
    {
        options.AddStringSecretOptions();
    });
});

builder.Services.AddDbContextFactory<AuthorityDbContext>(options =>
{
    string? cs = builder.Configuration.GetConnectionString("authoritydb");

    options.UseSqlite(cs);
});

builder.Services.AddAuthorityApplication();

ConsoleApplication app = builder.Build();

await app.RunLexicomTuiAsync();
