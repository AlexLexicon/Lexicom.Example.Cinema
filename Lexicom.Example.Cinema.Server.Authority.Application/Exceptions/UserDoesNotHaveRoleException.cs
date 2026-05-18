namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class UserDoesNotHaveRoleException(Guid userId, Guid roleId) : Exception($"The user with the id '{userId}' does not have the role with the id '{roleId}'.")
{
}
