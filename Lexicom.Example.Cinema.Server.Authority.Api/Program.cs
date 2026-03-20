using Lexicom.AspNetCore.Controllers.Amenities.Extensions;
using Lexicom.Authentication.For.AspNetCore.Controllers.Extensions;
using Lexicom.Authority.AspNetCore.Controllers.Extensions;
using Lexicom.Authority.Extensions;
using Lexicom.Authorization.AspNetCore.Controllers.Extensions;
using Lexicom.Cryptography.AspNetCore.Controllers.Extensions;
using Lexicom.Cryptography.Extensions;
using Lexicom.DependencyInjection.Primitives.Extensions;
using Lexicom.DependencyInjection.Primitives.For.AspNetCore.Controllers.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Api;
using Lexicom.Example.Cinema.Server.Authority.Application.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Database.Extensions;
using Lexicom.Example.Cinema.Server.Shared.Authentication;
using Lexicom.Logging.AspNetCore.Controllers.Extensions;
using Lexicom.Scalar.Extensions;
using Lexicom.Smtp.AspNetCore.Controllers.Extensions;
using Lexicom.Smtp.Extensions;
using Lexicom.Supports.AspNetCore.Controllers.Extensions;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.For.AspNetCore.Controllers.Extensions;
using System.Diagnostics;

/*
 * Lexicom.Example.Cinema.Server.Authority.Api
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
            Debugger.Break();
        });
#endif
        options.AddInvalidModelStateFactory();
    });
    options.AddAuthority(options =>
    {
        options.AddAccessTokenProvider();
        options.AddRefreshTokenProvider();
    });
    options.AddAuthentication(options =>
    {
        options.AddAccessTokenAuthentication();
    });
    options.AddAuthorization(options =>
    {
        options.AddPermissions(Policies.Permissions.All);
    });
    options.AddScalar();
    options.AddValidation(options =>
    {
        options.AddAmenities();
        options.AddRequestBodyActionFilter();
        options.AddValidators<AssemblyScanMarker>();
    });
    options.AddLogging();
    options.AddSmtp(options =>
    {
        options.AddFileClient();
    });
    options.AddPrimitives(options =>
    {
        options.AddTimeProvider();
        options.AddGuidProvider();
    });
    options.AddCryptography(options =>
    {
        options.AddStringSecretOptions();
    });
});

builder.Services.AddAuthorityDatabase();
builder.Services.AddAuthorityApplication();

var app = builder.Build();

app.UseLexicomExceptionHandlingMiddleware();
app.UseLexicomLogging();
app.UseLexicomScalar();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
