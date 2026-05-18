namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class RefreshTokenExpiredException(Guid refreshTokenId) : Exception($"The refresh token with the id '{refreshTokenId}' has expired.")
{
}
