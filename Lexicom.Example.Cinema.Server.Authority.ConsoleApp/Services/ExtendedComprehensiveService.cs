using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Models;
using Lexicom.Example.Cinema.Server.Authority.Database;
using Lexicom.Example.Cinema.Server.Authority.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;

public interface IExtendedComprehensiveService
{
    Task<ExtendedComprehensiveUser> GetExtendedComprehensiveUserAsync(Guid userId);
    public Task<IReadOnlyList<ExtendedComprehensiveUser>> GetExtendedComprehensiveUsersAsync();
    Task<ExtendedComprehensiveRole> GetExtendedComprehensiveRoleAsync(Guid roleId);
    Task<IReadOnlyList<ExtendedComprehensiveRole>> GetExtendedComprehensiveRolesAsync();
}
public class ExtendedComprehensiveService : IExtendedComprehensiveService
{
    private readonly IDbContextFactory<AuthorityDbContext> _dbContextFactory;
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IDateTimeService _dateTimeService;

    public ExtendedComprehensiveService(
        IDbContextFactory<AuthorityDbContext> dbContextFactory,
        IUserService userService,
        IRoleService roleService,
        IDateTimeService dateTimeService)
    {
        _dbContextFactory = dbContextFactory;
        _userService = userService;
        _roleService = roleService;
        _dateTimeService = dateTimeService;
    }

    public async Task<ExtendedComprehensiveUser> GetExtendedComprehensiveUserAsync(Guid userId)
    {
        ComprehensiveUser comprehensiveUser = await _userService.GetComprehensiveUserAsync(userId);

        var whenCreatedTask = _dateTimeService.GetLocalDateTimeStringFromUtcAsync(comprehensiveUser.CreatedDateTimeOffsetUtc);
        var whenVerifiedTask = _dateTimeService.GetLocalDateTimeStringFromUtcAsync(comprehensiveUser.VerifiedDateTimeOffsetUtc);
        var whenLastSignInTask = _dateTimeService.GetLocalDateTimeStringFromUtcAsync(comprehensiveUser.LastSignInDateTimeOffsetUtc);
        var lockedOutUntilTask = _dateTimeService.GetLocalDateTimeStringFromUtcAsync(comprehensiveUser.LockoutEndDateTimeOffsetUtc);

        string whenCreated = await whenCreatedTask;
        string whenVerified = await whenVerifiedTask;
        string whenLastSignIn = await whenLastSignInTask;
        string lockedOutUntil = await lockedOutUntilTask;

        return new ExtendedComprehensiveUser
        {
            Id = comprehensiveUser.Id,
            Email = comprehensiveUser.Email,
            FirstName = comprehensiveUser.FirstName,
            LastName = comprehensiveUser.LastName,
            CreatedDateTimeOffsetUtc = comprehensiveUser.CreatedDateTimeOffsetUtc,
            VerifiedDateTimeOffsetUtc = comprehensiveUser.VerifiedDateTimeOffsetUtc,
            LastSignInDateTimeOffsetUtc = comprehensiveUser.LastSignInDateTimeOffsetUtc,
            LockoutEndDateTimeOffsetUtc = comprehensiveUser.LockoutEndDateTimeOffsetUtc,
            WhenCreated = whenCreated,
            WhenVerified = whenVerified,
            WhenLastSignIn = whenLastSignIn,
            LockedOutUntil = lockedOutUntil,
            Roles = comprehensiveUser.Roles,
        };
    }

    public async Task<IReadOnlyList<ExtendedComprehensiveUser>> GetExtendedComprehensiveUsersAsync()
    {
        using var db = await _dbContextFactory.CreateDbContextAsync();

        var extendedComprehensiveUsers = new List<ExtendedComprehensiveUser>();

        List<User> users = await db.Users.ToListAsync();
        foreach (User user in users)
        {
            ExtendedComprehensiveUser extendedComprehensiveUser = await GetExtendedComprehensiveUserAsync(user.Id);

            extendedComprehensiveUsers.Add(extendedComprehensiveUser);
        }

        return extendedComprehensiveUsers;
    }

    public async Task<ExtendedComprehensiveRole> GetExtendedComprehensiveRoleAsync(Guid roleId)
    {
        ComprehensiveRole comprehensiveRole = await _roleService.GetComprehensiveRoleAsync(roleId);

        string whenCreated = await _dateTimeService.GetLocalDateTimeStringFromUtcAsync(comprehensiveRole.CreatedDateTimeOffsetUtc);

        return new ExtendedComprehensiveRole
        {
            Id = comprehensiveRole.Id,
            Name = comprehensiveRole.Name,
            CreatedDateTimeOffsetUtc = comprehensiveRole.CreatedDateTimeOffsetUtc,
            WhenCreated = whenCreated,
            Permissions = comprehensiveRole.Permissions,
        };
    }

    public async Task<IReadOnlyList<ExtendedComprehensiveRole>> GetExtendedComprehensiveRolesAsync()
    {
        using var db = await _dbContextFactory.CreateDbContextAsync();

        var extendedComprehensiveRoles = new List<ExtendedComprehensiveRole>();

        List<Role> roles = await db.Roles.ToListAsync();
        foreach (Role role in roles)
        {
            ExtendedComprehensiveRole extendedComprehensiveRole = await GetExtendedComprehensiveRoleAsync(role.Id);

            extendedComprehensiveRoles.Add(extendedComprehensiveRole);
        }

        return extendedComprehensiveRoles;
    }
}
