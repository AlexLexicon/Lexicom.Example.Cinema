namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class RefreshTokenAccessTokenSubjectMismatchException(Guid accessSubjectId, Guid refreshSubjectId) : Exception($"The access bearer token had the subject id '{accessSubjectId}' which is diffrent from the refresh bearer token subject id '{refreshSubjectId}'.")
{
}
