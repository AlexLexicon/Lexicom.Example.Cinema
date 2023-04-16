namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class EmailConfirmationTokenNotValidException : Exception
{
    public EmailConfirmationTokenNotValidException() : base("The email confirmation token is not valid.")
    {
    }
}
