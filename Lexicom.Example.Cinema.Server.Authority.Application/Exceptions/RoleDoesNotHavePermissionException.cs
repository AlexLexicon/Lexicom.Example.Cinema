namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class RoleDoesNotHavePermissionException : Exception
{
    public RoleDoesNotHavePermissionException(Guid roleId, string permission) : base($"The role with the id '{roleId}' does not have this permission '{permission}'.")
    {
    }
}
