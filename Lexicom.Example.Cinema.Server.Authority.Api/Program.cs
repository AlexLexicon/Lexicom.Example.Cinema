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
using Lexicom.Example.Cinema.Server.Authority.Database;
using Lexicom.Example.Cinema.Server.Authority.Database.Extensions;
using Lexicom.Example.Cinema.Server.Shared.Authentication;
using Lexicom.Example.Cinema.Server.Shared.Extensions;
using Lexicom.Logging.For.AspNetCore.Controllers.Extensions;
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

builder.Lexicom(l =>
{
    l.AddAmenities(a =>
    {
        a.AddErrorResponseActionFilter();
        a.AddExceptionHandlingMiddleware();
#if DEBUG
        a.DebugExceptionHandlingMiddleware(e =>
        {
            Debugger.Break();
        });
#endif
        a.AddInvalidModelStateFactory();
    });
    l.AddAuthority(auth =>
    {
        auth.AddAccessTokenProvider();
        auth.AddRefreshTokenProvider();
    });
    l.AddAuthentication(auth =>
    {
        auth.AddAccessTokenAuthentication();
    });
    l.AddAuthorization(auth =>
    {
        auth.AddPermissions(Policies.Permissions.All);
    });
    l.AddScalar();
    l.AddValidation(v =>
    {
        v.AddAmenities();
        v.AddRequestBodyActionFilter();
        v.AddValidators<AssemblyScanMarker>();
    });
    l.AddLogging();
    l.AddSmtp(smtp =>
    {
        smtp.AddFileClient();
    });
    l.AddPrimitives(p =>
    {
        p.AddTimeProvider();
        p.AddGuidProvider();
    });
    l.AddCryptography(c =>
    {
        c.AddStringSecretOptions();
    });
});

builder.Services.AddAuthorityDatabase();
builder.Services.AddAuthorityApplication();

var app = builder.Build();

await app.Services.EnsureDatabaseCreatedAsync<AuthorityDbContext>();
app.UseLexicomExceptionHandlingMiddleware();
app.UseLexicomLogging();
app.UseLexicomScalar();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
