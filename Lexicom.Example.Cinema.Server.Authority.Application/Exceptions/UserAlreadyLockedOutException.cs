namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class UserAlreadyLockedOutException : Exception
{
    public UserAlreadyLockedOutException(Guid userId) : base($"The user with the id '{userId}' is already locked out.")
    {
    }
}
