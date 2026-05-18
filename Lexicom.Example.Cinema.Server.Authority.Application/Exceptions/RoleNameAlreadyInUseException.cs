namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class RoleNameAlreadyInUseException(string? roleName) : Exception($"The role with the name '{roleName ?? "null"}' is already in use.")
{
}
