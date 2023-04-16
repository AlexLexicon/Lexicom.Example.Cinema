namespace Lexicom.Example.Cinema.Server.Authority.Application.Models;
public class RefreshToken
{
    public Guid Id { get; set; }
    public Guid AccessTokenJti { get; set; }
    public Guid UserId { get; set; }
    public DateTimeOffset CreatedDateTimeOffset { get; set; }
    public DateTimeOffset ExpiresDateTimeOffset { get; set; }
}
