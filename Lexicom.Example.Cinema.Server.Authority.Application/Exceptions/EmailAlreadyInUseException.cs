namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class EmailAlreadyInUseException : Exception
{
    public EmailAlreadyInUseException(string email) : base($"The email '{email}' is already in use.")
    {
    }
}
