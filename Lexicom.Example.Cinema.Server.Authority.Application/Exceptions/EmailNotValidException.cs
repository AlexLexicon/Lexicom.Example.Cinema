namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class EmailNotValidException : Exception
{
    public EmailNotValidException(string email) : base($"The email '{email}' is not valid.")
    {
    }
}
