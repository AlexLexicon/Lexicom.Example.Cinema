using Lexicom.Example.Cinema.Server.Authority.Application.Database;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;
public interface IComprehensiveService
{
    public Task<IReadOnlyList<ComprehensiveUser>> GetComprehensiveUsersAsync();
    Task<IReadOnlyList<ComprehensiveRole>> GetComprehensiveRolesAsync();
}
public class ComprehensiveService : IComprehensiveService
{
    private readonly IDbContextFactory<AuthorityDbContext> _dbContextFactory;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;

    public ComprehensiveService(
        IDbContextFactory<AuthorityDbContext> dbContextFactory,
        IUserService userService,
        IRoleService roleService)
    {
        _dbContextFactory = dbContextFactory;
        _userService = userService;
        _roleService = roleService;
    }

    public async Task<IReadOnlyList<ComprehensiveUser>> GetComprehensiveUsersAsync()
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

        return comprehensiveUsers;
    }

    public async Task<IReadOnlyList<ComprehensiveRole>> GetComprehensiveRolesAsync()
    {
        using var db = await _dbContextFactory.CreateDbContextAsync();

        var comprehensiveRoles = new List<ComprehensiveRole>();
        var roles = await db.Roles.ToListAsync();
        foreach (Role? role in roles)
        {
            if (role is not null)
            {
                ComprehensiveRole comprehensiveRole = await _roleService.GetComprehensiveRoleAsync(role.Id);

                comprehensiveRoles.Add(comprehensiveRole);
            }
        }

        return comprehensiveRoles;
    }
}
