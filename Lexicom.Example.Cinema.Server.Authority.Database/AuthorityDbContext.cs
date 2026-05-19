using Lexicom.Example.Cinema.Server.Authority.Database.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Authority.Database;

public class AuthorityDbContext : IdentityDbContext<User, Role, Guid>
{
    public AuthorityDbContext(DbContextOptions<AuthorityDbContext> options) : base(options)
    {
    }

    public DbSet<RefreshTokenEntry> RefreshTokenEntries { get; set; }
}
