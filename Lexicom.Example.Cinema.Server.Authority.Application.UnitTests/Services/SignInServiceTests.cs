namespace Lexicom.Example.Cinema.Server.Authority.Application.UnitTests.Services;
public class SignInServiceTests
{
    //private readonly UnitTestAttendant _test;
    //private ISignInService SignInService => _test.Get<ISignInService>();
    //private UserManager<User> UserManager => _test.Get<UserManager<User>>();

    //public SignInServiceTests()
    //{
    //    _test = new UnitTestAttendant();

    //    _test.AddTestAuthorityEntityFrameworkAndIdentity();

    //    _test.AddLogging();
    //    _test.AddTestProviders();

    //    _test.Mock<IUserService>();
    //    _test.Mock<IRoleService>();
    //    _test.Mock<IJwtService>();
    //    _test.Mock<IAccessTokenProvider>();

    //    _test.AddSingleton<ISignInService, SignInService>();
    //}

    //[Fact]
    //public async Task SignInUserAsync_Returns_BearerToken()
    //{
    //    //arrange
    //    string password = "Password1234!";
    //    string validBearerToken = "bearertoken";

    //    User dbUser = await UserManager.CreateAsync(password: password);
    //    await UserManager.ConfirmEmailAsync(dbUser);

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(dbUser);
    //    });
    //    _test.Mock<IAccessTokenProvider>(m =>
    //    {
    //        m.Setup(m => m.CreateAccessTokenAsync(It.IsAny<IEnumerable<Claim>>())).ReturnsAsync(new BearerToken(Guid.NewGuid(), DateTime.Now, validBearerToken));
    //    });

    //    //act
    //    var bearerToken = await SignInService.SignInUserAsync(dbUser.Email, password);

    //    //assert
    //    validBearerToken.Should().NotBeNull();
    //    validBearerToken.Should().Be(validBearerToken);
    //}

    //[Fact]
    //public async Task SignInUserAsync_Throws_UserLockedOutException()
    //{
    //    //arrange
    //    string password = "Password1234!";

    //    User dbUser = await UserManager.CreateAsync(password: password);
    //    await UserManager.ConfirmEmailAsync(dbUser);

    //    await UserManager.SetLockoutEndDateAsync(dbUser, DateTimeOffset.MaxValue);

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(dbUser);
    //    });

    //    //assert
    //    await Assert.ThrowsAsync<UserLockedOutException>(async () =>
    //    {
    //        //act
    //        await SignInService.SignInUserAsync(dbUser.Email, password);
    //    });
    //}

    //[Fact]
    //public async Task SignInUserAsync_Throws_UserNotVerifiedException()
    //{
    //    //arrange
    //    string password = "Password1234!";

    //    User dbUser = await UserManager.CreateAsync(password: password);

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(dbUser);
    //    });

    //    //assert
    //    await Assert.ThrowsAsync<UserNotVerifiedException>(async () =>
    //    {
    //        //act
    //        await SignInService.SignInUserAsync(dbUser.Email, password);
    //    });
    //}

    //[Fact]
    //public async Task SignInUserAsync_Throws_PasswordIncorrectException()
    //{
    //    //arrange
    //    string actualPassword = "Password1234!";
    //    string testPassword = "!4321drowssaP";

    //    User dbUser = await UserManager.CreateAsync(password: actualPassword);
    //    await UserManager.ConfirmEmailAsync(dbUser);

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(dbUser);
    //    });

    //    //assert
    //    await Assert.ThrowsAsync<PasswordIncorrectException>(async () =>
    //    {
    //        //act
    //        await SignInService.SignInUserAsync(dbUser.Email, testPassword);
    //    });
    //}
}
