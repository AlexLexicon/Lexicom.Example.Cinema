using Lexicom.AspNetCore.Controllers.Amenities.Extensions;
using Lexicom.Authentication.For.AspNetCore.Controllers.Extensions;
using Lexicom.Authority.AspNetCore.Controllers.Extensions;
using Lexicom.Authority.Extensions;
using Lexicom.Authorization.AspNetCore.Controllers.Extensions;
using Lexicom.Cryptography.AspNetCore.Controllers.Extensions;
using Lexicom.Cryptography.Extensions;
using Lexicom.DependencyInjection.Primitives.For.AspNetCore.Controllers.Extensions;
using Lexicom.EntityFramework.Identity.AspNetCore.Controllers.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Api;
using Lexicom.Example.Cinema.Server.Authority.Application.Database;
using Lexicom.Example.Cinema.Server.Authority.Application.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Shared.Authentication;
using Lexicom.Logging.AspNetCore.Controllers.Extensions;
using Lexicom.Smtp.AspNetCore.Controllers.Extensions;
using Lexicom.Smtp.Extensions;
using Lexicom.Supports.AspNetCore.Controllers.Extensions;
using Lexicom.Swashbuckle.Extensions;
using Lexicom.Validation.Amenities.Extensions;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.For.AspNetCore.Controllers.Extensions;
using Microsoft.EntityFrameworkCore;
using System.IdentityModel.Tokens.Jwt;

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
        options.ReplaceDefaultInboundClaimType(JwtRegisteredClaimNames.Sub);
    });
    options.AddAuthorization(options =>
    {
        options.AddPermissions(Policies.Permissions.All);
    });
    options.AddEntityFrameworkIdentity<AuthorityDbContext, User, Role, Guid>();
    options.AddSwaggerGen();
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
    options.AddTimeProvider();
    options.AddGuidProvider();
    options.AddCryptography(options =>
    {
        options.AddStringSecretOptions();
    });
});

builder.Services.AddDbContextFactory<AuthorityDbContext>(options =>
{
    string? cs = builder.Configuration.GetConnectionString("AuthorityDb");

    options.UseSqlite(cs);
});

builder.Services.AddAuthorityApplication();

var app = builder.Build();

app.UseLexicomExceptionHandlingMiddleware();
app.UseLexicomLogging();
app.UseLexicomSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
