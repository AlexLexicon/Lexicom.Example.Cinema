namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class RefreshTokenDoesNotExistException(Guid refreshTokenId) : Exception($"The user refresh token with the id '{refreshTokenId}' does not exist.")
{
}
