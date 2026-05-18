namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class UserNotVerifiedException(Guid userId) : Exception($"The user with the id '{userId}' is not verified.")
{
}
