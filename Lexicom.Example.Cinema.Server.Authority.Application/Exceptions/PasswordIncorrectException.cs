namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class PasswordIncorrectException : Exception
{
    public PasswordIncorrectException() : base("The password was incorrect.")
    {
    }
}
