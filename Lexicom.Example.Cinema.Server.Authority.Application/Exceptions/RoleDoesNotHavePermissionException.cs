namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class RoleDoesNotHavePermissionException(Guid roleId, string? permission) : Exception($"The role with the id '{roleId}' does not have this permission '{permission ?? "null"}'.")
{
}
