using Lexicom.Example.Cinema.Server.Authority.Database;
using Lexicom.Example.Cinema.Server.Authority.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Services;

public interface IRefreshTokenService
{
    Task RemoveRefreshTokenAsync(Guid userId);
}
public class RefreshTokenService : IRefreshTokenService
{
    private readonly IDbContextFactory<AuthorityDbContext> _dbContextFactory;

    public RefreshTokenService(IDbContextFactory<AuthorityDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task RemoveRefreshTokenAsync(Guid userId)
    {
        using var db = await _dbContextFactory.CreateDbContextAsync();

        RefreshToken? dbRefreshToken = await db.RefreshTokens.FirstOrDefaultAsync(urt => urt.UserId == userId);

        if (dbRefreshToken is not null)
        {
            db.RefreshTokens.Remove(dbRefreshToken);
            await db.SaveChangesAsync();
        }
    }
}
