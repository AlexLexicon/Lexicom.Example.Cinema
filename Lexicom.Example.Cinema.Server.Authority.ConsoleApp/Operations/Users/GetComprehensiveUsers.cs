using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Database;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Users;
[TuiPage("Users")]
public class GetComprehensiveUsers : ITuiOperation
{
    private readonly IDbContextFactory<AuthorityDbContext> _dbContextFactory;
    private readonly IUserService _userService;

    public GetComprehensiveUsers(
        IDbContextFactory<AuthorityDbContext> dbContextFactory,
        IUserService userService)
    {
        _dbContextFactory = dbContextFactory;
        _userService = userService;
    }

    public async Task ExecuteAsync()
    {
        using var db = await _dbContextFactory.CreateDbContextAsync();

        List<User> users = await db.Users.ToListAsync();

        var comprehensiveUsers = new List<ComprehensiveUser>();
        foreach (User user in users)
        {
            ComprehensiveUser comprehensiveUser = await _userService.GetComprehensiveUserAsync(user.Id);

            comprehensiveUsers.Add(comprehensiveUser);
        }

        Console.WriteLine("ComprehensiveUsers:");
        Consolex.WriteAsJson(comprehensiveUsers);
        Console.WriteLine();
    }
}
