namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class UserLockedOutException : Exception
{
    public UserLockedOutException(Guid userId) : base($"The user with the id '{userId}' is locked out.")
    {
    }
}
