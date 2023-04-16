using Lexicom.Example.Cinema.Server.Authority.Application.Database;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Lexicom.Example.Cinema.Server.Authority.Application.UnitTests.Testing.Extensions;
public static class UserManagerExtensions
{
    public static async Task<User> CreateAsync(this UserManager<User> userManager, Guid? id = null, string? email = null, string password = "Password1234!", string? firstName = null, string? lastName = null)
    {
        id ??= Guid.NewGuid();
        email ??= $"{Guid.NewGuid().ToString().ToLowerInvariant()}@email.com";
        firstName ??= $"first{Guid.NewGuid().ToString().ToLowerInvariant()}name";
        lastName ??= $"last{Guid.NewGuid().ToString().ToLowerInvariant()}name";

        var user = new User
        {
            Id = id.Value,
            UserName = id.ToString(),
            Email = email,
            FirstNameEncryptedBase64 = firstName,
            LastNameEncryptedBase64 = lastName,
        };

        await userManager.CreateAsync(user, password);

        return user;
    }

    public static async Task<List<Role>> GetRolesAsync(this AuthorityDbContext db, User user)
    {
        return await db.UserRoles
            .Where(ur => ur.UserId == user.Id)
            .Join(db.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r)
            .ToListAsync();
    }

    public static async Task ConfirmEmailAsync(this UserManager<User> userManager, User user)
    {
        string emailConfirmationToken = await userManager.GenerateEmailConfirmationTokenAsync(user);

        await userManager.ConfirmEmailAsync(user, emailConfirmationToken);
    }
}
