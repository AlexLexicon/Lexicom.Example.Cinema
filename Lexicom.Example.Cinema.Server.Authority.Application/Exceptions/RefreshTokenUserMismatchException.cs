namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class RefreshTokenUserMismatchException : Exception
{
    public RefreshTokenUserMismatchException(Guid refreshTokenId, Guid refreshTokenUserId, Guid accessTokenUserId) : base($"The refresh token with the id '{refreshTokenId}' has a user id of '{refreshTokenUserId}' which is diffrent from the access token's user id '{accessTokenUserId}'.")
    {
    }
}
