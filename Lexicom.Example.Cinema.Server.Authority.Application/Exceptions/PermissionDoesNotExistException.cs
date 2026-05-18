namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class PermissionDoesNotExistException(string? permission) : Exception($"The permission '{permission ?? "null"}' does not exist.")
{
}
