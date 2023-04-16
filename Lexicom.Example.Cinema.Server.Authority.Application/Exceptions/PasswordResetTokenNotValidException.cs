namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class PasswordResetTokenNotValidException : Exception
{
    public PasswordResetTokenNotValidException() : base("The password reset token is not valid.")
    {
    }
}
