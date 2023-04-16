namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class RefreshTokenAccessTokenJtiMismatchException : Exception
{
    public RefreshTokenAccessTokenJtiMismatchException(Guid refreshTokenId, Guid refreshTokenAccessTokenJti, Guid accessTokenJti) : base($"The refresh token '{refreshTokenId}' had a access token jti of '{refreshTokenAccessTokenJti}' which was different from the actual access token jti '{accessTokenJti}'.")
    {
    }
}
