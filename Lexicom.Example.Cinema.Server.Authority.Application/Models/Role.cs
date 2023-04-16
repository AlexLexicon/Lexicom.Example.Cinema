using Lexicom.EntityFramework.Amenities.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Models;
public class Role : IdentityRole<Guid>
{
    public DateTimeOffset CreatedDateTimeOffsetUtc { get; set; }

    //in this application role names are a required field of the role
    //we can be safe in assuming it will never be null
    //it would be best to manually edit the database table and not allow nulls
    public override string Name
    {
        get => base.Name ?? throw new NonNullableTableColumnException(Name);
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        set => base.Name = value;
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    }

    public override string NormalizedName
    {
        get => base.NormalizedName ?? throw new NonNullableTableColumnException(NormalizedName);
#pragma warning disable CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
        set => base.NormalizedName = value;
#pragma warning restore CS8765 // Nullability of type of parameter doesn't match overridden member (possibly because of nullability attributes).
    }
}
