using Lexicom.Jwt.Exceptions;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class AccessBearerTokenNotValidException : BearerTokenNotValidException
{
    public AccessBearerTokenNotValidException(string bearerToken) : base(bearerToken, "access")
    {
        
    }
}
