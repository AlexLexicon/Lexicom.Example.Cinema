namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class UserNotLockedOutException : Exception
{
    public UserNotLockedOutException(Guid userId) : base($"The user with the id '{userId}' is not locked out.")
    {
    }
}
