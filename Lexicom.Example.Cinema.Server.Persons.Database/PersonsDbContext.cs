using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Persons.Database;

public class PersonsDbContext : DbContext
{
    public PersonsDbContext(DbContextOptions options) : base(options)
    {
        Database.EnsureCreated();
    }
}
