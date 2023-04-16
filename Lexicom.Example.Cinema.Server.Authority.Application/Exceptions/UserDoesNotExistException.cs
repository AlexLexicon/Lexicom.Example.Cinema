namespace Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
public class UserDoesNotExistException : Exception
{
    public UserDoesNotExistException(Guid userId) : base($"The user with the id '{userId}' does not exist.")
    {
    }
    public UserDoesNotExistException(string email) : base($"The user with the email '{email}' does not exist.")
    {
    }
}
