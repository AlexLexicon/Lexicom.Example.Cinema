using Lexicom.Authority;
using Lexicom.DependencyInjection.Primitives;
using Lexicom.Example.Cinema.Server.Authority.Database;
using Lexicom.Example.Cinema.Server.Authority.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Services;

public interface IRefreshTokenEntryService
{
    Task RemoveRefreshTokenEntriesAsync(Guid userId);
    Task CreateRefreshTokenEntryAsync(Guid userId, Guid accessTokenJti, BearerToken bearerToken);
}
public class RefreshTokenEntryService : IRefreshTokenEntryService
{
    private readonly IDbContextFactory<AuthorityDbContext> _dbContextFactory;
    private readonly ITimeProvider _timeProvider;

    public RefreshTokenEntryService(
        IDbContextFactory<AuthorityDbContext> dbContextFactory,
        ITimeProvider timeProvider)
    {
        _dbContextFactory = dbContextFactory;
        _timeProvider = timeProvider;
    }

    public async Task RemoveRefreshTokenEntriesAsync(Guid userId)
    {
        using var db = await _dbContextFactory.CreateDbContextAsync();

        bool isRemoved = await RemoveRefreshTokenEntriesAsync(db, userId);
        if (isRemoved)
        {
            await db.SaveChangesAsync();
        }
    }

    public async Task CreateRefreshTokenEntryAsync(Guid userId, Guid accessTokenJti, BearerToken bearerToken)
    {
        using var db = await _dbContextFactory.CreateDbContextAsync();

        await RemoveRefreshTokenEntriesAsync(db, userId);

        var userRefreshToken = new RefreshTokenEntry
        {
            Id = bearerToken.Jti,
            UserId = userId,
            AccessTokenJti = accessTokenJti,
            CreatedDateTimeOffsetUtc = _timeProvider.GetUtcNow(),
            ExpiresDateTimeOffsetUtc = bearerToken.Expires,
        };

        await db.RefreshTokenEntries.AddAsync(userRefreshToken);

        await db.SaveChangesAsync();
    }

    private async Task<bool> RemoveRefreshTokenEntriesAsync(AuthorityDbContext db, Guid userId)
    {
        List<RefreshTokenEntry> usersRefreshTokens = await db.RefreshTokenEntries
            .Where(urt => urt.UserId == userId)
            .ToListAsync();

        if (usersRefreshTokens.Count is > 0)
        {
            db.RefreshTokenEntries.RemoveRange(usersRefreshTokens);

            return true;
        }

        return false;
    }
}
