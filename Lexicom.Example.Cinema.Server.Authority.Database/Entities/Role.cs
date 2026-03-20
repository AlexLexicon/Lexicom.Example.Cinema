using Lexicom.EntityFramework.Amenities.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Lexicom.Example.Cinema.Server.Authority.Database.Entities;

public class Role : IdentityRole<Guid>
{
    public DateTimeOffset CreatedDateTimeOffsetUtc { get; init; }

    public override string Name
    {
        get => base.Name ?? throw new NonNullableTableColumnException(Name);
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        set
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        {
            if (value is null)
            {
                throw new NonNullableTableColumnException(nameof(Name));
            }

            base.Name = value;
        }
    }

    public override string NormalizedName
    {
        get => base.NormalizedName ?? throw new NonNullableTableColumnException(NormalizedName);
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        set
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        {
            if (value is null)
            {
                throw new NonNullableTableColumnException(nameof(NormalizedName));
            }

            base.NormalizedName = value;
        }
    }
}
