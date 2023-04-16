using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Database;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Roles;
[TuiPage("Roles")]
public class CreateRole : ITuiOperation
{
    private readonly IDbContextFactory<AuthorityDbContext> _dbContextFactory;
    private readonly IRoleService _roleService;

    public CreateRole(
        IDbContextFactory<AuthorityDbContext> dbContextFactory,
        IRoleService roleService)
    {
        _dbContextFactory = dbContextFactory;
        _roleService = roleService;
    }

    public async Task ExecuteAsync()
    {
        using var db = await _dbContextFactory.CreateDbContextAsync();

        List<string> roleNames = await db.Roles
            .Select(r => r.Name)
            .ToListAsync();

        if (roleNames.Any())
        {
            Console.WriteLine("Current Roles:");
            for (int count = 0; count < roleNames.Count; count++)
            {
                Console.WriteLine($"[{count}]: {roleNames[count]}");
            }
            Console.WriteLine();
        }

        string name = Consolex.ReadLine("Enter the name of the role you want to create");

        Role role = await _roleService.CreateRoleAsync(name);

        Consolex.WriteAsJsonWithType(role);
    }
}
