using Lexicom.Cryptography;
using Lexicom.DependencyInjection.Primitives;
using Lexicom.EntityFramework.Amenities.Exceptions;
using Lexicom.EntityFramework.Identity.Exceptions;
using Lexicom.EntityFramework.Identity.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Application.Database;
using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Services;
public interface IUserService
{
    /// <exception cref="UserDoesNotExistException"/>
    Task<User> GetUserByIdAsync(Guid userId);
    /// <exception cref="UserDoesNotExistException"/>
    Task<User> GetUserByEmailAsync(string email);
    /// <exception cref="UserDoesNotExistException"/>
    Task<IReadOnlyList<Role>> GetUserRolesAsync(Guid userId);
    /// <exception cref="UserDoesNotExistException"/>
    Task<ComprehensiveUser> GetComprehensiveUserAsync(Guid userId);
    /// <exception cref="EmailAlreadyInUseException"/>
    /// <exception cref="EmailNotValidException"/>
    /// <exception cref="PasswordMissingRequirementsException"/>
    Task<User> CreateUserAsync(string email, string password, string firstName, string lastName);
    /// <exception cref="UserDoesNotExistException"/>
    Task UpdateUserAsync(Guid userId, string? newFirstName, string? newLastName);
    /// <exception cref="UserDoesNotExistException"/>
    /// <exception cref="RoleDoesNotExistException"/>
    /// <exception cref="UserAlreadyHasRoleException"/>
    Task AddRoleToUserAsync(Guid userId, Guid roleId);
    /// <exception cref="UserDoesNotExistException"/>
    /// <exception cref="RoleDoesNotExistException"/>
    /// <exception cref="UserDoesNotHaveRoleException"/>
    Task RemoveRoleFromUserAsync(Guid userId, Guid roleId);
}
public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly IRoleService _roleService;
    private readonly ICryptographyService _cryptographyService;
    private readonly IDbContextFactory<AuthorityDbContext> _dbContextFactory;
    private readonly ITimeProvider _timeProvider;
    private readonly IGuidProvider _guidProvider;

    public UserService(
        UserManager<User> userManager,
        IRoleService roleService,
        ICryptographyService cryptographyService,
        IDbContextFactory<AuthorityDbContext> dbContextFactory,
        ITimeProvider timeProvider,
        IGuidProvider guidProvider)
    {
        _userManager = userManager;
        _roleService = roleService;
        _cryptographyService = cryptographyService;
        _dbContextFactory = dbContextFactory;
        _timeProvider = timeProvider;
        _guidProvider = guidProvider;
    }

    public async Task<User> GetUserByIdAsync(Guid userId)
    {
        User? user = await _userManager.FindByIdAsync(userId.ToString());

        if (user is null)
        {
            throw new UserDoesNotExistException(userId);
        }

        return user;
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        User? user = await _userManager.FindByEmailAsync(email);

        if (user is null)
        {
            throw new UserDoesNotExistException(email);
        }

        return user;
    }

    public async Task<IReadOnlyList<Role>> GetUserRolesAsync(Guid userId)
    {
        User user = await GetUserByIdAsync(userId);

        using var db = await _dbContextFactory.CreateDbContextAsync();

        return await db.UserRoles
            .Where(ur => ur.UserId == user.Id)
            .Join(db.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r)
            .ToListAsync();
    }

    public async Task<ComprehensiveUser> GetComprehensiveUserAsync(Guid userId)
    {
        var getUserRolesTask = GetUserRolesAsync(userId);
        var getUserByIdTask = GetUserByIdAsync(userId);

        IReadOnlyList<Role> roles = await getUserRolesTask;
        var getComprehensiveUserRoleTasks = new List<Task<ComprehensiveRole>>();
        foreach (Role role in roles)
        {
            getComprehensiveUserRoleTasks.Add(_roleService.GetComprehensiveRoleAsync(role.Id));
        }

        ComprehensiveRole[] comprehensiveUserRoles = await Task.WhenAll(getComprehensiveUserRoleTasks);

        User user = await getUserByIdTask;

        var firstNameEncryptedBase64DecryptTask = _cryptographyService.DecryptAsync(user.FirstNameEncryptedBase64);
        var lastNameEncryptedBase64DecryptTask = _cryptographyService.DecryptAsync(user.LastNameEncryptedBase64);

        string firstName = await firstNameEncryptedBase64DecryptTask;
        string lastName = await lastNameEncryptedBase64DecryptTask;

        return new ComprehensiveUser
        {
            Id = user.Id,
            Email = user.Email,
            FirstName = firstName,
            LastName = lastName,
            CreatedDateTimeOffset = user.CreatedDateTimeOffsetUtc,
            VerifiedDateTimeOffset = user.VerifiedDateTimeOffsetUtc,
            LastSignInDateTimeOffset = user.LastSignInDateTimeOffsetUtc,
            Roles = comprehensiveUserRoles,
        };
    }

    public async Task<User> CreateUserAsync(string email, string password, string firstName, string lastName)
    {
        bool userWithEmailAlreadyExists = await _userManager.FindByEmailAsync(email) is not null;

        if (userWithEmailAlreadyExists)
        {
            throw new EmailAlreadyInUseException(email);
        }

        var firstNameEncryptTask = _cryptographyService.EncryptAsync(firstName);
        var lastNameEncryptTask = _cryptographyService.EncryptAsync(lastName);

        string firstNameEncryptedBase64 = await firstNameEncryptTask;
        string lastNameEncryptedBase64 = await lastNameEncryptTask;

        Guid userId = _guidProvider.NewGuid();

        var user = new User
        {
            Id = userId,
            UserName = userId.ToString(),
            Email = email,
            FirstNameEncryptedBase64 = firstNameEncryptedBase64,
            LastNameEncryptedBase64 = lastNameEncryptedBase64,
            CreatedDateTimeOffsetUtc = _timeProvider.UtcNow,
            VerifiedDateTimeOffsetUtc = null,
            LastSignInDateTimeOffsetUtc = null,
        };

        IdentityResult createIdentityResult = await _userManager.CreateAsync(user, password);

        if (!createIdentityResult.Succeeded)
        {
            if (createIdentityResult.HasInvalidEmailError())
            {
                throw new EmailNotValidException(email);
            }

            if (createIdentityResult.HasPasswordMissingRequirementsError())
            {
                throw new PasswordMissingRequirementsException();
            }

            throw new IdentityResultException(createIdentityResult);
        }

        return user;
    }

    public async Task UpdateUserAsync(Guid userId, string? newFirstName, string? newLastName)
    {
        User user = await GetUserByIdAsync(userId);

        if (newFirstName is not null)
        {
            user.FirstNameEncryptedBase64 = await _cryptographyService.EncryptAsync(newFirstName);
        }

        if (newLastName is not null)
        {
            user.LastNameEncryptedBase64 = await _cryptographyService.EncryptAsync(newLastName);
        }

        await _userManager.UpdateAsync(user);
    }

    public async Task AddRoleToUserAsync(Guid userId, Guid roleId)
    {
        User user = await GetUserByIdAsync(userId);

        Role role = await _roleService.GetRoleByIdAsync(roleId);

        NonNullableTableColumnException.ThrowIfNull(role.Name);

        IdentityResult addToRoleIdentityResult = await _userManager.AddToRoleAsync(user, role.Name);

        if (!addToRoleIdentityResult.Succeeded)
        {
            if (addToRoleIdentityResult.HasUserInRoleAlreadyError())
            {
                throw new UserAlreadyHasRoleException(userId, roleId);
            }

            throw new IdentityResultException(addToRoleIdentityResult);
        }
    }

    public async Task RemoveRoleFromUserAsync(Guid userId, Guid roleId)
    {
        User user = await GetUserByIdAsync(userId);

        Role role = await _roleService.GetRoleByIdAsync(roleId);

        NonNullableTableColumnException.ThrowIfNull(role.Name);

        IdentityResult removeFromRoleIdentityResult = await _userManager.RemoveFromRoleAsync(user, role.Name);

        if (!removeFromRoleIdentityResult.Succeeded)
        {
            if (removeFromRoleIdentityResult.HasUserNotInRoleError())
            {
                throw new UserDoesNotHaveRoleException(userId, roleId);
            }

            throw new IdentityResultException(removeFromRoleIdentityResult);
        }
    }
}
