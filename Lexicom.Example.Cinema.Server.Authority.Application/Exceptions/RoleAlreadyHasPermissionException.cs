namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class RoleAlreadyHasPermissionException(Guid roleId, string? permission) : Exception($"The role with the id '{roleId}' already has the permission '{permission ?? "null"}'.")
{
}
