namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class RoleNameNotValidException : Exception
{
    public RoleNameNotValidException(string name) : base($"The role name '{name}' is not valid.")
    {
    }
}