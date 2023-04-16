namespace Lexicom.Example.Cinema.Server.Authority.Application.UnitTests.Services;
public class RegistrationServiceTests
{
    //private readonly UnitTestAttendant _test;
    //private IRegistrationService RegistrationService => _test.Get<IRegistrationService>();
    //private UserManager<User> UserManager => _test.Get<UserManager<User>>();
    //private RoleManager<Role> RoleManager => _test.Get<RoleManager<Role>>();

    //public RegistrationServiceTests()
    //{
    //    _test = new UnitTestAttendant();

    //    _test.AddTestAuthorityEntityFrameworkAndIdentity();

    //    _test.AddLogging();
    //    _test.AddTestProviders();

    //    _test.Mock<IUserService>();
    //    _test.Mock<IRoleService>();
    //    _test.Mock<IVerificationService>();
    //    _test.Mock<IEmailService>();

    //    _test.AddSingleton<IRegistrationService, RegistrationService>();
    //}

    //[Fact]
    //public async Task RegisterUserAsync()
    //{
    //    //arrange
    //    string password = "Password1234!";
    //    string emailConfirmationToken = "7343041436ae44409d6828acbe957497";

    //    User dbUser = await UserManager.CreateAsync(password: password);
    //    Role dbRole = await RoleManager.CreateAsync();

    //    object isEmailSent = false;

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.CreateUserAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(dbUser);
    //    });
    //    _test.Mock<IRoleService>(m =>
    //    {
    //        m.Setup(m => m.GetRoleByNameAsync(It.IsAny<string>())).ReturnsAsync(dbRole);
    //    });
    //    _test.Mock<IVerificationService>(m =>
    //    {
    //        m.Setup(m => m.CreateUserEmailConfirmationTokenAsync(It.IsAny<Guid>())).ReturnsAsync(emailConfirmationToken);
    //    });
    //    _test.Mock<IEmailService>(m =>
    //    {
    //        m.Setup(m => m.SendConfirmationEmailAsync(It.IsAny<Guid>(), It.IsAny<string>())).Returns(() =>
    //        {
    //            isEmailSent = true;

    //            return Task.CompletedTask;
    //        });
    //    });

    //    //act
    //    await RegistrationService.RegisterUserAsync(dbUser.Email, password, dbUser.FirstName, dbUser.LastName);

    //    //assert
    //    ((bool)isEmailSent).Should().BeTrue();
    //}
}
