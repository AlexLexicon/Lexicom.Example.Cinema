namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class UserNotVerifiedException : Exception
{
    public UserNotVerifiedException(Guid userId) : base($"The user with the id '{userId}' is not verified.")
    {
    }
}
