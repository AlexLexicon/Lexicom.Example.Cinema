namespace Lexicom.Example.Cinema.Server.Authority.Database.Entities;

public class RefreshToken
{
    public required Guid Id { get; init; }
    public required Guid UserId { get; init; }

    public required Guid AccessTokenJti { get; set; }

    public required DateTimeOffset CreatedDateTimeOffsetUtc { get; init; }
    public required DateTimeOffset ExpiresDateTimeOffsetUtc { get; set; }
}
