namespace Lexicom.Example.Cinema.Server.Authority.Application.UnitTests.Services;
public class PasswordServiceTests
{
    //private readonly UnitTestAttendant _test;
    //private IPasswordService PasswordService => _test.Get<IPasswordService>();
    //private UserManager<User> UserManager => _test.Get<UserManager<User>>();

    //public PasswordServiceTests()
    //{
    //    _test = new UnitTestAttendant();

    //    _test.AddTestAuthorityEntityFrameworkAndIdentity();

    //    _test.AddLogging();
    //    _test.AddTestProviders();

    //    _test.Mock<IUserService>();
    //    _test.Mock<IEmailService>();

    //    _test.AddSingleton<IPasswordService, PasswordService>();
    //}

    //[Fact]
    //public async Task ChangeUserPasswordAsync()
    //{
    //    //arrange
    //    string currentPassword = "Password1234!";
    //    string newPassword = "!4321drowssaP";

    //    User dbUser = await UserManager.CreateAsync(password: currentPassword);

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dbUser);
    //    });

    //    //act
    //    await PasswordService.ChangeUserPasswordAsync(dbUser.Id, currentPassword, newPassword);

    //    //assert
    //    (await UserManager.CheckPasswordAsync(dbUser, currentPassword)).Should().BeFalse();
    //    (await UserManager.CheckPasswordAsync(dbUser, newPassword)).Should().BeTrue();
    //}

    //[Fact]
    //public async Task ChangeUserPasswordAsync_Throws_PasswordMissingRequirementsException()
    //{
    //    //arrange
    //    string currentPassword = "Password1234!";
    //    string newPassword = "abc";

    //    User dbUser = await UserManager.CreateAsync(password: currentPassword);

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dbUser);
    //    });

    //    //assert
    //    await Assert.ThrowsAsync<PasswordMissingRequirementsException>(async () =>
    //    {
    //        //act
    //        await PasswordService.ChangeUserPasswordAsync(dbUser.Id, currentPassword, newPassword);
    //    });
    //}

    //[Fact]
    //public async Task ChangeUserPasswordAsync_Throws_PasswordIncorrectException()
    //{
    //    //arrange
    //    string correctCurrentPassword = "Password1234!";
    //    string wrongCurrentPassword = "1234!Password";
    //    string newPassword = "!4321drowssaP";

    //    User dbUser = await UserManager.CreateAsync(password: correctCurrentPassword);

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dbUser);
    //    });

    //    //assert
    //    await Assert.ThrowsAsync<PasswordIncorrectException>(async () =>
    //    {
    //        //act
    //        await PasswordService.ChangeUserPasswordAsync(dbUser.Id, wrongCurrentPassword, newPassword);
    //    });
    //}

    ////[Fact]
    ////public async Task RequestForgotPasswordEmailAsync()
    ////{
    ////    //arrange
    ////    User dbUser = await UserManager.CreateAsync();

    ////    object resetToken = "";

    ////    _test.Mock<IUserService>(m =>
    ////    {
    ////        m.Setup(m => m.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(dbUser);
    ////    });

    ////    _test.Mock<IEmailService>(m =>
    ////    {
    ////        m.Setup(m => m.SendForgotPasswordEmailAsync(It.IsAny<Guid>(), It.IsAny<string>())).Returns<Guid, string>((userId, passwordResetToken) =>
    ////        {
    ////            resetToken = passwordResetToken;

    ////            return Task.CompletedTask;
    ////        });
    ////    });

    ////    //act
    ////    await PasswordService.RequestUserForgotPasswordEmailAsync(dbUser.Email);

    ////    //assert
    ////    ((string)resetToken).Should().NotBeEmpty();
    ////}

    //[Fact]
    //public async Task ResetUserPasswordAsync()
    //{
    //    //arrange
    //    string originalPassword = "Password1234!";
    //    string newPassword = "!4321reipS";

    //    User dbUser = await UserManager.CreateAsync(password: originalPassword);

    //    string passwordResetToken = await UserManager.GeneratePasswordResetTokenAsync(dbUser);

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(dbUser);
    //    });

    //    //act
    //    await PasswordService.ResetUserPasswordAsync(dbUser.Email, passwordResetToken, newPassword);

    //    //assert
    //    (await UserManager.CheckPasswordAsync(dbUser, originalPassword)).Should().BeFalse();
    //    (await UserManager.CheckPasswordAsync(dbUser, newPassword)).Should().BeTrue();
    //}

    //[Fact]
    //public async Task ResetUserPasswordAsync_Throws_PasswordMissingRequirementsException()
    //{
    //    //arrange
    //    string newPassword = "abc";

    //    User dbUser = await UserManager.CreateAsync();

    //    string passwordResetToken = await UserManager.GeneratePasswordResetTokenAsync(dbUser);

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(dbUser);
    //    });

    //    //assert
    //    await Assert.ThrowsAsync<PasswordMissingRequirementsException>(async () =>
    //    {
    //        //act
    //        await PasswordService.ResetUserPasswordAsync(dbUser.Email, passwordResetToken, newPassword);
    //    });
    //}

    //[Fact]
    //public async Task ResetUserPasswordAsync_Throws_PasswordResetTokenInvalidException()
    //{
    //    //arrange
    //    string passwordResetToken = "abc";
    //    string newPassword = "!4321reipS";

    //    User dbUser = await UserManager.CreateAsync();

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(dbUser);
    //    });

    //    //assert
    //    await Assert.ThrowsAsync<PasswordResetTokenNotValidException>(async () =>
    //    {
    //        //act
    //        await PasswordService.ResetUserPasswordAsync(dbUser.Email, passwordResetToken, newPassword);
    //    });
    //}
}
