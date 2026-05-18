namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class EmailAlreadyInUseException(string? email) : Exception($"The email '{email ?? "null"}' is already in use.")
{
}
