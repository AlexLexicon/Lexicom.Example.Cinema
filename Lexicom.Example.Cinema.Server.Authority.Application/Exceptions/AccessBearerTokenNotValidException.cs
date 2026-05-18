using Lexicom.Jwt.Exceptions;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class AccessBearerTokenNotValidException(string? bearerToken) : BearerTokenNotValidException(bearerToken, "access")
{
}
