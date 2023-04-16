namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class RefreshTokenExpiredException : Exception
{
    public RefreshTokenExpiredException(Guid refreshTokenId) : base($"The refresh token with the id '{refreshTokenId}' has expired.")
    {
    }
}
