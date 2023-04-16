namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class PermissionDoesNotExistException : Exception
{
    public PermissionDoesNotExistException(string permission) : base($"The permission '{permission}' does not exist.")
    {
    }
}
