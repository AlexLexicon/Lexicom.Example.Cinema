namespace Lexicom.Example.Cinema.Server.Authority.Api.Contracts.Errors;
public static class AuthorityErrorCodes
{
    public const string TOKEN_INVALID = "token:invalid";

    public const string USER_CREDENTIALS_INCORRECT = "user:credentials:incorrect";

    public const string USER_PASSWORD_REQUIREMENTS_MISSING = "user:password:requirements:missing";
    public const string USER_PASSWORD_INCORRECT = "user:password:incorrect";

    public const string USER_EMAIL_UNAVAILABLE = "user:email:unavailable";
    public const string USER_EMAIL_INVALID = "user:email:invalid";
    public const string USER_EMAIL_CONFIRMED = "user:email:confirmed";

    public const string USER_MODERATION_LOCKED = "user:moderation:locked";

    public const string USER_VERIFICATION_INCOMPLETE = "user:verification:incomplete";
}
