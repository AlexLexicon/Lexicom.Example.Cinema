using Lexicom.Example.Cinema.Server.Shared.Authentication;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Services;
public interface IPermissionService
{
    Task<bool> PermissionExistsAsync(string permission);
}
public class PermissionService : IPermissionService
{
    private readonly IReadOnlyList<string> _permissions;

    public PermissionService()
    {
        _permissions = Policies.Permissions.All;
    }

    public Task<bool> PermissionExistsAsync(string permission)
    {
        bool permissionExists = _permissions.Contains(permission);

        return Task.FromResult(permissionExists);
    }
}
