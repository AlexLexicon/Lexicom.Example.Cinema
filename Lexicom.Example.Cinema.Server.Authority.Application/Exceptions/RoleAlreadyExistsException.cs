namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class RoleAlreadyExistsException(string? roleName) : Exception($"The role with the name '{roleName ?? "null"}' already exists.")
{
}
