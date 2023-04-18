using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Database;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Users;
[TuiPage("Users")]
public class GetRefreshTokenForUser : ITuiOperation
{
    private readonly IDbContextFactory<AuthorityDbContext> _dbContextFactory;
    private readonly IComprehensiveService _comprehensiveService;

    public GetRefreshTokenForUser(
        IDbContextFactory<AuthorityDbContext> dbContextFactory, 
        IComprehensiveService comprehensiveService)
    {
        _dbContextFactory = dbContextFactory;
        _comprehensiveService = comprehensiveService;
    }

    public async Task ExecuteAsync()
    {
        IReadOnlyList<ComprehensiveUser> comprehensiveUsers = await _comprehensiveService.GetComprehensiveUsersAsync();
        Console.WriteLine("Avaliable Users:");
        Consolex.WriteAsJson(comprehensiveUsers);
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
