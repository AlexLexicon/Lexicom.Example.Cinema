using Lexicom.Example.Cinema.Server.Authority.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Authority.Application.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Shared.Authentication;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Services;
public interface IRegistrationService
{
    /// <exception cref="EmailAlreadyInUseException"/>
    /// <exception cref="EmailNotValidException"/>
    /// <exception cref="PasswordMissingRequirementsException"/>
    Task RegisterUserAsync(string email, string password, string firstName, string lastName);
}
public class RegistrationService : IRegistrationService
{
    private readonly IRoleService _roleService;
    private readonly IUserService _userService;
    private readonly ICommunicationService _communicationService;

    public RegistrationService(
        IRoleService roleService,
        IUserService userService,
        ICommunicationService communicationService)
    {
        _roleService = roleService;
        _userService = userService;
        _communicationService = communicationService;
    }

    public async Task RegisterUserAsync(string email, string password, string firstName, string lastName)
    {
        var createUserTask = _userService.CreateUserAsync(email, password, firstName, lastName);
        var getBasicRoleTask = _roleService.GetRoleByNameAsync(Roles.BASIC_NAME);

        User user = await createUserTask;

        Role basicRole;
        try
        {
            basicRole = await getBasicRoleTask;
        }
        catch (RoleDoesNotExistException e)
        {
            throw e.ToUnreachableException();
        }

        var addBasicRoleToUserTask = _userService.AddRoleToUserAsync(user.Id, basicRole.Id);
        var createEmailConfirmationTokenTask = _communicationService.SendUserConfirmEmailCommunciationAsync(user.Id);

        try
        {
            await addBasicRoleToUserTask;
        }
        catch (UserDoesNotExistException e)
        {
            throw e.ToUnreachableException();
        }
        catch (RoleDoesNotExistException e)
        {
            throw e.ToUnreachableException();
        }
        catch (UserAlreadyHasRoleException e)
        {
            throw e.ToUnreachableException();
        }

        try
        {
            await createEmailConfirmationTokenTask;
        }
        catch (UserDoesNotExistException e)
        {
            throw e.ToUnreachableException();
        }
    }
}
