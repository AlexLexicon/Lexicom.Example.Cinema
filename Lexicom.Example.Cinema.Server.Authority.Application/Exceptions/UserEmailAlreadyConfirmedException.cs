namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class UserEmailAlreadyConfirmedException : Exception
{
    public UserEmailAlreadyConfirmedException(Guid userId) : base($"The user with the id '{userId}' already has their email confirmed.")
    {
    }
}
