namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class UserNotLockedOutException(Guid userId) : Exception($"The user with the id '{userId}' is not locked out.")
{
}
