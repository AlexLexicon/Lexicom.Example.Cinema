using MediatR;

namespace Lexicom.Example.Cinema.Client.Application.Mediator;
public record class SignInFailedNotification : INotification
{
    public SignInFailedNotification(Errors error)
    {
        Error = error;  
    }

    public Errors Error { get; }

    public enum Errors
    {
        IncorrectCredentials,
        LockedOut,
        NotVerified,
    }
}
