using Lexicom.Example.Cinema.Client.Application.Models;

namespace Lexicom.Example.Cinema.Client.Application.Services;

public interface IUserService
{
    Task<User?> GetLoggedInUserAsync();
}
public class UserService : IUserService
{
    public Task<User?> GetLoggedInUserAsync()
    {
        User? user = null;

        return Task.FromResult(user);
    }
}
