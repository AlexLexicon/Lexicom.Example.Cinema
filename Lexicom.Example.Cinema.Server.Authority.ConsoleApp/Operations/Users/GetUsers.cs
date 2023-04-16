using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Database;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Users;
[TuiPage("Users")]
public class GetUsers : ITuiOperation
{
    private readonly IDbContextFactory<AuthorityDbContext> _dbContextFactory;

    public GetUsers(IDbContextFactory<AuthorityDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task ExecuteAsync()
    {
        using var db = await _dbContextFactory.CreateDbContextAsync();

        List<User> users = await db.Users.ToListAsync();

        Console.WriteLine("Users:");
        Consolex.WriteAsJson(users);
        Console.WriteLine();
    }
}