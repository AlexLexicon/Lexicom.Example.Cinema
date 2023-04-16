using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Database;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Roles;
[TuiPage("Roles")]
public class GetRoles : ITuiOperation
{
    private readonly IDbContextFactory<AuthorityDbContext> _dbContextFactory;

    public GetRoles(IDbContextFactory<AuthorityDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }

    public async Task ExecuteAsync()
    {
        using var db = await _dbContextFactory.CreateDbContextAsync();

        List<Role> roles = await db.Roles.ToListAsync();

        Console.WriteLine("Roles:");
        Consolex.WriteAsJson(roles);
        Console.WriteLine();
    }
}
