namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class UserLockedOutException(Guid userId) : Exception($"The user with the id '{userId}' is locked out.")
{
}
