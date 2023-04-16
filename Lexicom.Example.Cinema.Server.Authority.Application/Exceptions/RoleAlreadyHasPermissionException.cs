namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class RoleAlreadyHasPermissionException : Exception
{
    public RoleAlreadyHasPermissionException(Guid roleId, string permission) : base($"The role with the id '{roleId}' already has the permission '{permission}'.")
    {
    }
}
