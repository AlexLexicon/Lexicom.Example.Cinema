using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Database;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Users;
[TuiPage("Users")]
public class AddRoleToUser : ITuiOperation
{
    private readonly IDbContextFactory<AuthorityDbContext> _dbContextFactory;
    private readonly IUserService _userService;

    public AddRoleToUser(
        IDbContextFactory<AuthorityDbContext> dbContextFactory,
        IUserService userService)
    {
        _dbContextFactory = dbContextFactory;
        _userService = userService;
    }

    public async Task ExecuteAsync()
    {
        using var db = await _dbContextFactory.CreateDbContextAsync();

        var comprehensiveUsers = new List<ComprehensiveUser>();
        var users = await db.Users.ToListAsync();
        foreach (User? user in users)
        {
            if (user is not null)
            {
                ComprehensiveUser comprehensiveUser = await _userService.GetComprehensiveUserAsync(user.Id);

                comprehensiveUsers.Add(comprehensiveUser);
            }
        }

        Console.WriteLine("Users:");
        Consolex.WriteAsJson(comprehensiveUsers);

        Guid userId = Consolex.ReadLineGuid("Enter the id of the user you want to add a role to:");

        var aRoles = await db.Roles
            .Select(r => new
            {
                r.Id,
                r.Name,
            }).
            ToListAsync();

        Console.WriteLine("Roles:");
        Consolex.WriteAsJson(aRoles);

        Guid roleId = Consolex.ReadLineGuid("Enter the id of the role you want to add to the user:");

        await _userService.AddRoleToUserAsync(userId, roleId);

        ComprehensiveUser newComprehensiveUser = await _userService.GetComprehensiveUserAsync(userId);

        Consolex.WriteAsJsonWithType(newComprehensiveUser);
    }
}
