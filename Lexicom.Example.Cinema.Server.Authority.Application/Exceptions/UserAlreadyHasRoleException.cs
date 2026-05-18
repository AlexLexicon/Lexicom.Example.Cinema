namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class UserAlreadyHasRoleException(Guid userId, Guid roleId) : Exception($"The user with the id '{userId}' already has the role with the id '{roleId}'.")
{
}
