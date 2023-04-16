using Lexicom.Example.Cinema.Server.Movies.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Movies.Application.Database;
public class MoviesDbContext : DbContext
{
    public MoviesDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<Movie> Movies { get; set; }
    public DbSet<Review> Ratings { get; set; }
}
