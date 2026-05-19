using Lexicom.ConsoleApp.Amenities;
using Lexicom.ConsoleApp.Amenities.ReadLines.Settings;
using Lexicom.ConsoleApp.Tui;
using Lexicom.Example.Cinema.Server.Authority.Application.Models;
using Lexicom.Example.Cinema.Server.Authority.Application.Services;

namespace Lexicom.Example.Cinema.Server.Authority.ConsoleApp.Operations.Users;

[TuiPage("Users")]
public class Login : ITuiOperation
{
    private readonly ISignInService _signInService;

    public Login(ISignInService signInService)
    {
        _signInService = signInService;
    }

    public async Task ExecuteAsync()
    {
        string email = Consolex.ReadLine("Enter the email of the user you want to login:", new ReadLineSettings
        {
            DefaultInput = "test_a@email.com",
        });
        string password = Consolex.ReadLine($"Enter the password for the user with the email '{email}':", new ReadLineSettings
        {
            DefaultInput = "Password1234!",
        });

        SignIn signIn = await _signInService.SignInUserAsync(email, password);

        Console.WriteLine("SignIn:");
        Consolex.WriteAsJson(signIn);
    }
}