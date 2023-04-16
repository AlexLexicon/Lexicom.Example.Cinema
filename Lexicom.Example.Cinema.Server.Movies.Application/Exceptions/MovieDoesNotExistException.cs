namespace Lexicom.Example.Cinema.Server.Movies.Application.Exceptions;
public class MovieDoesNotExistException : Exception
{
    public MovieDoesNotExistException(Guid movieId) : base($"The movie with the id '{movieId}' does not exist.")
    {
    }
}
