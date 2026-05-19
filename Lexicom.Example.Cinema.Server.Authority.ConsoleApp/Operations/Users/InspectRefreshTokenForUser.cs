using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Models;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;
using Lexicom.Example.Cinema.Server.Authority.Database;
using Lexicom.Example.Cinema.Server.Authority.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Users;

[TuiPage("Users")]
public class InspectRefreshTokenForUser : ITuiOperation
{
    private readonly IDbContextFactory<AuthorityDbContext> _dbContextFactory;
    private readonly IExtendedComprehensiveService _extendedComprehensiveService;

    public InspectRefreshTokenForUser(
        IDbContextFactory<AuthorityDbContext> dbContextFactory,
        IExtendedComprehensiveService extendedComprehensiveService)
    {
        _dbContextFactory = dbContextFactory;
        _extendedComprehensiveService = extendedComprehensiveService;
    }

    public async Task ExecuteAsync()
    {
        IReadOnlyList<ExtendedComprehensiveUser> extendedComprehensiveUsers = await _extendedComprehensiveService.GetExtendedComprehensiveUsersAsync();
        Console.WriteLine("Avaliable Users:");
        Consolex.WriteAsJson(extendedComprehensiveUsers);
        Console.WriteLine();

        Guid userId = Consolex.ReadLineGuid("Enter the id of the user you want to get the refresh token for:");
        Console.WriteLine();

        using var db = await _dbContextFactory.CreateDbContextAsync();

        RefreshToken? refreshToken = await db.RefreshTokens.FirstOrDefaultAsync(rt => rt.UserId == userId);

        if (refreshToken is null)
        {
            Console.WriteLine($"The user with the id '{userId}' does not have a refresh token.");
        }
        else
        {
            Consolex.WriteAsJsonWithType(refreshToken);
        }
    }
}
