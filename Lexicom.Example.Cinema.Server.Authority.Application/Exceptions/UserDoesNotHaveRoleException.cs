namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class UserDoesNotHaveRoleException : Exception
{
    public UserDoesNotHaveRoleException(Guid userId, Guid roleId) : base($"The user with the id '{userId}' does not have the role with the id '{roleId}'.")
    {
    }
}
