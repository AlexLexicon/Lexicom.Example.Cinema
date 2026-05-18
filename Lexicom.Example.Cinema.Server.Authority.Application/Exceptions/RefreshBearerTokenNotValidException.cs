using Lexicom.Jwt.Exceptions;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class RefreshBearerTokenNotValidException(string? bearerToken) : BearerTokenNotValidException(bearerToken, "refresh")
{
}
