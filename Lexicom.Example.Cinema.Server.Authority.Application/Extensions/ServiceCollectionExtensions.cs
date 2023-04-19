using Lexicom.DependencyInjection.Amenities.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Application.Options;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Validation.Extensions;
using Lexicom.Validation.Options.Extensions;
using Microsoft.Extensions.DependencyInjection;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Extensions;
public static class ServiceCollectionExtensions
{
    public static void AddAuthorityApplication(this IServiceCollection services)
    {
        services.AddLexicomValidation(options =>
        {
            options.AddValidators<AssemblyScanMarker>();
        });

        services
            .AddOptions<BrandOptions>()
            .BindConfiguration()
            .Validate()
            .ValidateOnStart();
        services
            .AddOptions<UrlsOptions>()
            .BindConfiguration()
            .Validate()
            .ValidateOnStart();

        services.AddScoped<ICommunicationService, CommunicationService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IModerationService, ModerationService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IRegistrationService, RegistrationService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<ISignInService, SignInService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IVerificationService, VerificationService>();
    }
}
