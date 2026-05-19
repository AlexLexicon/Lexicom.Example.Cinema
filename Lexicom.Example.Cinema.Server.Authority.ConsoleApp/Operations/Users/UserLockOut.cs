using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;
using Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Users;

[TuiPage("Users")]
public class UserLockOut : ITuiOperation
{
    private readonly IComprehensiveService _comprehensiveService;
    private readonly IUserService _userService;

    public UserLockOut(
        IComprehensiveService comprehensiveService,
        IUserService userService)
    {
        _comprehensiveService = comprehensiveService;
        _userService = userService;
    }

    public async Task ExecuteAsync()
    {
        IReadOnlyList<ComprehensiveUser> comprehensiveUsers = await _comprehensiveService.GetComprehensiveUsersAsync();
        Console.WriteLine("Avaliable Users:");
        Consolex.WriteAsJson(comprehensiveUsers);
        Console.WriteLine();

        Guid userId = Consolex.ReadLineGuid("Enter the id of the user you want to add a role to:");
        Console.WriteLine();

        bool lockUser = Consolex
            .BinaryQuestion()
            .SetTrue("Lock")
            .SetFalse("UnLock")
            .Ask("What do you want to do to the user?");
        Console.WriteLine();

        if (lockUser)
        {
            DateTimeOffset lockoutEndDate = Consolex.ReadLineDateTimeOffset("Enter the date time you want to lock out the user until");

            await _userService.LockUserAsync(userId, lockoutEndDate);
        }
        else
        {
            await _userService.UnLockUserAsync(userId);
        }
    }
}
