namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;

public class PasswordMissingRequirementsException() : Exception("The password did not meet the secuirty requirements.")
{
}