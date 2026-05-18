namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class RoleNameNotValidException(string? name) : Exception($"The role name '{name ?? "null"}' is not valid.")
{
}