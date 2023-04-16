namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class RefreshTokenDoesNotExistException : Exception
{
    public RefreshTokenDoesNotExistException(Guid refreshTokenId) : base($"The user refresh token with the id '{refreshTokenId}' does not exist.")
    {
    }
}
