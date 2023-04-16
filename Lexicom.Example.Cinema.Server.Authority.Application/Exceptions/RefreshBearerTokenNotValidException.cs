using Lexicom.Jwt.Exceptions;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class RefreshBearerTokenNotValidException : BearerTokenNotValidException
{
    public RefreshBearerTokenNotValidException(string bearerToken) : base(bearerToken, "refresh")
    {
    }
}
