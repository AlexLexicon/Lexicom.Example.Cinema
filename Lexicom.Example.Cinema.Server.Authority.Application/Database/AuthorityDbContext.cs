using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Database;
public class AuthorityDbContext : IdentityDbContext<User, Role, Guid>
{
    public AuthorityDbContext(DbContextOptions<AuthorityDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<RefreshToken> RefreshTokens { get; set; }
}
