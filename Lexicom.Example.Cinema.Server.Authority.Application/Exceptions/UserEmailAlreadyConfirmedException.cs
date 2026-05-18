namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class UserEmailAlreadyConfirmedException(Guid userId) : Exception($"The user with the id '{userId}' already has their email confirmed.")
{
}
