namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class UserAlreadyLockedOutException(Guid userId) : Exception($"The user with the id '{userId}' is already locked out.")
{
}
