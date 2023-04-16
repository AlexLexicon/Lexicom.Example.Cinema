namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class PasswordMissingRequirementsException : Exception
{
    public PasswordMissingRequirementsException() : base("The password did not meet the secuirty requirements.")
    {
    }
}