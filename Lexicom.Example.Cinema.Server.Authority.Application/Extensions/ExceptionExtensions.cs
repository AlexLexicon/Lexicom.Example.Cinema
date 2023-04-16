using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Extensions.Exceptions;
using System.Diagnostics;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Extensions;
public static class ExceptionExtensions
{
    public static UnreachableException ToUnreachableException(this UserDoesNotExistException e)
    {
        return e.ToUnreachableException("The user must exist at this point.");
    }

    public static UnreachableException ToUnreachableException(this RoleDoesNotExistException e)
    {
        return e.ToUnreachableException("The role must exist at this point.");
    }

    public static UnreachableException ToUnreachableException(this RefreshTokenDoesNotExistException e)
    {
        return e.ToUnreachableException("The refresh token must exist at this point.");
    }

    public static UnreachableException ToUnreachableException(this UserAlreadyHasRoleException e)
    {
        return e.ToUnreachableException("The user cannot already have the role at this point.");
    }
}
