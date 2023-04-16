using Lexicom.EntityFramework.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Models;
public class User : IdentityUser<Guid>
{
    public string FirstNameEncryptedBase64 { get; set; } = null!;
    public string LastNameEncryptedBase64 { get; set; } = null!;
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
    public DateTimeOffset? VerifiedDateTimeOffset { get; set; }
    public DateTimeOffset? LastSignInDateTimeOffset { get; set; }

    //in this application emails are a required field of the user
    //we can be safe in assuming it will never be null
    //it would be best to manually edit the database table and not allow nulls
    public override string Email
    {
        get => base.Email ?? throw new NonNullableTableColumnException(Email);
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        set => base.Email = value;
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    }
    public override string NormalizedEmail
    {
        get => base.NormalizedEmail ?? throw new NonNullableTableColumnException(NormalizedEmail);
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        set => base.NormalizedEmail = value;
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    }
}
