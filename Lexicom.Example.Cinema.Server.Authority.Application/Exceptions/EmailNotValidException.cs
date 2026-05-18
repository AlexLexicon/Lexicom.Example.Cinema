namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class EmailNotValidException(string? email) : Exception($"The email '{email ?? "null"}' is not valid.")
{
}
