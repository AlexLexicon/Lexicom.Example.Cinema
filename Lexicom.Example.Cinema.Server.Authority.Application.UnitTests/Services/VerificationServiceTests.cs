namespace Lexicom.Example.Cinema.Server.Authority.Application.UnitTests.Services;
public class VerificationServiceTests
{
    //private readonly UnitTestAttendant _test;
    //private IVerificationService VerificationService => _test.Get<IVerificationService>();
    //private UserManager<User> UserManager => _test.Get<UserManager<User>>();

    //public VerificationServiceTests()
    //{
    //    _test = new UnitTestAttendant();

    //    _test.AddTestAuthorityEntityFrameworkAndIdentity();

    //    _test.AddLogging();
    //    _test.AddTestProviders();

    //    _test.Mock<IUserService>();

    //    _test.AddSingleton<IVerificationService, VerificationService>();
    //}

    //[Fact]
    //public async Task CreateUserEmailConfirmationTokenAsync()
    //{
    //    //arrange
    //    User dbUser = await UserManager.CreateAsync();

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dbUser);
    //    });

    //    //act
    //    string emailConfirmationToken = await VerificationService.CreateUserEmailConfirmationTokenAsync(dbUser.Id);

    //    //assert
    //    await UserManager.ConfirmEmailAsync(dbUser, emailConfirmationToken);

    //    (await UserManager.IsEmailConfirmedAsync(dbUser)).Should().BeTrue();
    //}

    //[Fact]
    //public async Task ConfirmUserEmailAsync()
    //{
    //    //arrange
    //    User dbUser = await UserManager.CreateAsync();

    //    string emailConfirmationToken = await UserManager.GenerateEmailConfirmationTokenAsync(dbUser);

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(dbUser);
    //    });

    //    //act
    //    await VerificationService.ConfirmUserEmailAsync(dbUser.Email, emailConfirmationToken);

    //    //assert
    //    (await UserManager.IsEmailConfirmedAsync(dbUser)).Should().BeTrue();
    //}

    //[Fact]
    //public async Task ConfirmUserEmailAsync_Throws_EmailConfirmationTokenInvalidException()
    //{
    //    //arrange
    //    User dbUser = await UserManager.CreateAsync();

    //    string emailConfirmationToken = "abc";

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(dbUser);
    //    });

    //    //assert
    //    await Assert.ThrowsAsync<EmailConfirmationTokenNotValidException>(async () =>
    //    {
    //        //act
    //        await VerificationService.ConfirmUserEmailAsync(dbUser.Email, emailConfirmationToken);
    //    });
    //}

    //[Fact]
    //public async Task CreateUserEmailChangeTokenAsync()
    //{
    //    //arrange
    //    string originalEmail = "test@email.com";
    //    string newEmail = "new@email.com";

    //    User dbUser = await UserManager.CreateAsync(email: originalEmail);

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dbUser);
    //    });

    //    //act
    //    string emailChangeToken = await VerificationService.CreateUserEmailChangeTokenAsync(dbUser.Id, newEmail);

    //    //assert
    //    await UserManager.ChangeEmailAsync(dbUser, newEmail, emailChangeToken);

    //    dbUser.Email.Should().NotBe(originalEmail);
    //    dbUser.Email.Should().Be(newEmail);
    //}

    //[Fact]
    //public async Task ChangeUserEmailAsync()
    //{
    //    //arrange
    //    string originalEmail = "test@email.com";
    //    string newEmail = "new@email.com";

    //    User dbUser = await UserManager.CreateAsync(email: originalEmail);

    //    string emailChangeToken = await UserManager.GenerateChangeEmailTokenAsync(dbUser, newEmail);

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dbUser);
    //    });

    //    //act
    //    await VerificationService.ChangeUserEmailAsync(dbUser.Id, newEmail, emailChangeToken);

    //    //assert
    //    (await UserManager.IsEmailConfirmedAsync(dbUser)).Should().BeTrue();
    //}

    //[Fact]
    //public async Task ChangeUserEmailAsync_Throws_EmailChangeTokenInvalidException_Beecause_Wrong_Email()
    //{
    //    //arrange
    //    string otherEmail = "other@email.com";
    //    string newEmail = "new@email.com";

    //    User dbUser = await UserManager.CreateAsync();

    //    string emailChangeToken = await UserManager.GenerateChangeEmailTokenAsync(dbUser, otherEmail);

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dbUser);
    //    });

    //    //assert
    //    await Assert.ThrowsAsync<EmailChangeTokenNotValidException>(async () =>
    //    {
    //        //act
    //        await VerificationService.ChangeUserEmailAsync(dbUser.Id, newEmail, emailChangeToken);
    //    });
    //}

    //[Fact]
    //public async Task ChangeUserEmailAsync_Throws_EmailChangeTokenInvalidException_Because_Bad_Token()
    //{
    //    //arrange
    //    string newEmail = "new@email.com";

    //    User dbUser = await UserManager.CreateAsync();

    //    string emailChangeToken = "abc";

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dbUser);
    //    });

    //    //assert
    //    await Assert.ThrowsAsync<EmailChangeTokenNotValidException>(async () =>
    //    {
    //        //act
    //        await VerificationService.ChangeUserEmailAsync(dbUser.Id, newEmail, emailChangeToken);
    //    });
    //}
}
