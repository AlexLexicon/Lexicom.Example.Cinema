namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class RefreshTokenUserMismatchException(Guid refreshTokenId, Guid refreshTokenUserId, Guid accessTokenUserId) : Exception($"The refresh token with the id '{refreshTokenId}' has a user id of '{refreshTokenUserId}' which is diffrent from the access token's user id '{accessTokenUserId}'.")
{
}
