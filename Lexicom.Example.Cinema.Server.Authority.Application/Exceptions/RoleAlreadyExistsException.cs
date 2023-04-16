namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class RoleAlreadyExistsException : Exception
{
    public RoleAlreadyExistsException(Guid roleId) : base($"The role with the id '{roleId}' already exists.")
    {
    }
    public RoleAlreadyExistsException(string roleName) : base($"The role with the name '{roleName}' already exists.")
    {
    }
}
