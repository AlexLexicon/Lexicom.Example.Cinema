namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class RoleNameAlreadyInUseException : Exception
{
    public RoleNameAlreadyInUseException(string roleName) : base($"The role with the name '{roleName}' is already in use.")
    {
    }
}
