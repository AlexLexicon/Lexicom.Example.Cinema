namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class RoleDoesNotExistException : Exception
{
    public RoleDoesNotExistException(Guid roleId) : base($"The role with the id '{roleId}' does not exist.")
    {
    }
    public RoleDoesNotExistException(string roleName) : base($"The role with the name '{roleName}' does not exist.")
    {
    }
}
