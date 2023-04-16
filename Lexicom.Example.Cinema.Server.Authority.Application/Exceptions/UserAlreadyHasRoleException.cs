namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class UserAlreadyHasRoleException : Exception
{
    public UserAlreadyHasRoleException(Guid userId, Guid roleId) : base($"The user with the id '{userId}' already has the role with the id '{roleId}'.")
    {
    }
}
