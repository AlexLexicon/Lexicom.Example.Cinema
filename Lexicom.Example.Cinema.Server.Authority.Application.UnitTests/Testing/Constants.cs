using Microsoft.AspNetCore.Identity;

namespace Lexicom.Example.Cinema.Server.Authority.Application.UnitTests.Testing;
internal static class Constants
{
    public static readonly TimeSpan IDENTITYCONFIGURATION_LOCKOUTCONFIGURATION_DEFAULTLOCKOUTTIMESPAN = TimeSpan.FromSeconds(1);
    public static readonly TimeSpan IDENTITYCONFIGURATION_PASSWORDCONFIGURATION_RESETCONFIGURATION_TOKENVALIDTIMESPAN = TimeSpan.FromSeconds(1);
    public static readonly IdentityOptions IDENTITYCONFIGURATION = new IdentityOptions
    {
        ClaimsIdentity = new ClaimsIdentityOptions
        {
            RoleClaimType = "http://schemas.microsoft.com/ws/2008/06/identity/claims/role",
            UserNameClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name",
            UserIdClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier",
            EmailClaimType = "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress",
            SecurityStampClaimType = "AspNet.Identity.SecurityStamp",
        },
        User = new UserOptions
        {
            AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+",
            RequireUniqueEmail = true,//false
        },
        Password = new PasswordOptions
        {
            RequiredLength = 6,
            RequiredUniqueChars = 1,
            RequireNonAlphanumeric = true,
            RequireLowercase = true,
            RequireUppercase = true,
            RequireDigit = true
        },
        Lockout = new LockoutOptions
        {
            AllowedForNewUsers = true,//false
            MaxFailedAccessAttempts = 5,
            DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5),
        },
        SignIn = new SignInOptions
        {
            RequireConfirmedEmail = true,//false
            RequireConfirmedPhoneNumber = false,
            RequireConfirmedAccount = false,
        },
        Tokens = new TokenOptions
        {
            EmailConfirmationTokenProvider = TokenOptions.DefaultProvider,
            PasswordResetTokenProvider = TokenOptions.DefaultProvider,
            ChangeEmailTokenProvider = TokenOptions.DefaultProvider,
            ChangePhoneNumberTokenProvider = TokenOptions.DefaultPhoneProvider,
            AuthenticatorTokenProvider = TokenOptions.DefaultAuthenticatorProvider,
            AuthenticatorIssuer = "Microsoft.AspNetCore.Identity.UI"
        },
        Stores = new StoreOptions
        {
            MaxLengthForKeys = 0,
            ProtectPersonalData = false,
        },
    };
}
