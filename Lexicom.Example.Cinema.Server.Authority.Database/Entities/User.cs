using Lexicom.EntityFramework.Amenities.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Lexicom.Example.Cinema.Server.Authority.Database.Entities;

public class User : IdentityUser<Guid>
{
    public required string FirstNameEncryptedBase64 { get; set; }
    public required string LastNameEncryptedBase64 { get; set; }
    public required DateTimeOffset? VerifiedDateTimeOffsetUtc { get; set; }
    public required DateTimeOffset? LastSignInDateTimeOffsetUtc { get; set; }

    public required DateTimeOffset CreatedDateTimeOffsetUtc { get; init; }

    public override string Email
    {
        get => base.Email ?? throw new NonNullableTableColumnException(Email);
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        set
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        {
            if (value is null)
            {
                throw new NonNullableTableColumnException(nameof(Email));
            }

            base.Email = value;
        }
    }

    public override string NormalizedEmail
    {
        get => base.NormalizedEmail ?? throw new NonNullableTableColumnException(NormalizedEmail);
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        set
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        {
            if (value is null)
            {
                throw new NonNullableTableColumnException(nameof(NormalizedEmail));
            }

            base.NormalizedEmail = value;
        }
    }
}
