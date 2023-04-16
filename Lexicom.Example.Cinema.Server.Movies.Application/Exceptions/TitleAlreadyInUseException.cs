namespace Lexicom.Example.Cinema.Server.Movies.Application.Exceptions;
public class TitleAlreadyInUseException : Exception
{
    public TitleAlreadyInUseException(string title) : base($"A movie with the title '{title}' already exists.")
    {
    }
}
