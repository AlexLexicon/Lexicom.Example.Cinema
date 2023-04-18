using Lexicom.AspNetCore.Controllers.Amenities.Extensions;
using Lexicom.Authentication.For.AspNetCore.Controllers.Extensions;
using Lexicom.Authorization.AspNetCore.Controllers.Extensions;
using Lexicom.DependencyInjection.Primitives.Extensions;
using Lexicom.DependencyInjection.Primitives.For.AspNetCore.Controllers.Extensions;
using Lexicom.Example.Cinema.Server.Movies.Api;
using Lexicom.Example.Cinema.Server.Movies.Api.Contracts.Extensions;
using Lexicom.Example.Cinema.Server.Movies.Application.Database;
using Lexicom.Example.Cinema.Server.Movies.Application.Extensions;
using Lexicom.Example.Cinema.Server.Shared.Authentication;
using Lexicom.Logging.AspNetCore.Controllers.Extensions;
using Lexicom.Smtp.AspNetCore.Controllers.Extensions;
using Lexicom.Supports.AspNetCore.Controllers.Extensions;
using Lexicom.Swashbuckle.Extensions;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.For.AspNetCore.Controllers.Extensions;
using Microsoft.EntityFrameworkCore;

/*
 * Lexicom.Example.Cinema.Server.Movies.Api
 */

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.SecretsExample.json");

builder.Services.AddControllers();

builder.Lexicom(options =>
{
    options.AddAmenities(options =>
    {
        options.AddErrorResponseActionFilter();
        options.AddExceptionHandlingMiddleware();
#if DEBUG
        options.DebugExceptionHandlingMiddleware(e =>
        {

        });
#endif
        options.AddInvalidModelStateFactory();
    });
    options.AddAuthentication(options =>
    {
        options.AddAccessTokenAuthentication();
    });
    options.AddAuthorization(options =>
    {
        options.AddPermissions(Policies.Permissions.All);
    });
    options.AddSwaggerGen();
    options.AddValidation(options =>
    {
        options.AddAmenities();
        options.AddRequestBodyActionFilter();
        options.AddValidators<AssemblyScanMarker>();
        options.AddMoviesApiRuleSets();
    });
    options.AddLogging();
    options.AddPrimitives(options =>
    {
        options.AddTimeProvider();
        options.AddGuidProvider();
    });
});

builder.Services.AddDbContextFactory<MoviesDbContext>(options =>
{
    string? cs = builder.Configuration.GetConnectionString("MoviesDb");

    options.UseSqlite(cs);
});

builder.Services.AddMoviesApplication();

var app = builder.Build();

app.UseLexicomExceptionHandlingMiddleware();
app.UseLexicomLogging();
app.UseLexicomSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
