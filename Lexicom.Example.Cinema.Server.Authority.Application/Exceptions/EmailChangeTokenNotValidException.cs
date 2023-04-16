namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class EmailChangeTokenNotValidException : Exception
{
    public EmailChangeTokenNotValidException() : base("The email change token is not valid.")
    {
    }
}
