namespace Lexicom.Example.Cinema.Server.Authority.Application.UnitTests.Services;
public class EmailServiceTests
{
    //private readonly UnitTestAttendant _test;
    //private IEmailService EmailService => _test.Get<IEmailService>();
    //private UserManager<User> UserManager => _test.Get<UserManager<User>>();

    //public EmailServiceTests()
    //{
    //    _test = new UnitTestAttendant();

    //    _test.AddTestAuthorityEntityFrameworkAndIdentity();

    //    _test.AddLogging();
    //    _test.AddTestProviders();

    //    _test.MockOptions(new BrandOptions
    //    {
    //        AppName = "app",
    //        CompanyName = "company",
    //    });
    //    _test.MockOptions(new UrlsOptions
    //    {
    //        ConfirmationEmailUrl = "www.test.com/confirm",
    //        ForgotPasswordEmailUrl = "www.test.com/forgot",
    //    });

    //    _test.Mock<IUserService>();
    //    _test.Mock<ISmtpEmailHandler>();

    //    _test.AddSingleton<IEmailService, EmailService>();
    //}

    //[Fact]
    //public async Task SendForgotPasswordEmailAsync()
    //{
    //    //arrange
    //    string passwordResetToken = "passwordresettoken";

    //    User dbUser = await UserManager.CreateAsync();

    //    object isSmtpEmailHandled = false;

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dbUser);
    //    });
    //    _test.Mock<ISmtpEmailHandler>(m =>
    //    {
    //        m.Setup(m => m.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns<string, string, string>((toEmailAddress, subject, body) =>
    //        {
    //            isSmtpEmailHandled = true;

    //            return Task.CompletedTask;
    //        });
    //    });

    //    //act
    //    await EmailService.SendForgotPasswordEmailAsync(dbUser.Id, passwordResetToken);

    //    //assert
    //    ((bool)isSmtpEmailHandled).Should().BeTrue();
    //}

    //[Fact]
    //public async Task SendConfirmationEmailAsync()
    //{
    //    //arrange
    //    string emailConfirmationToken = "emailconfirmationtoken";

    //    User dbUser = await UserManager.CreateAsync();

    //    object isSmtpEmailHandled = false;

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dbUser);
    //    });
    //    _test.Mock<ISmtpEmailHandler>(m =>
    //    {
    //        m.Setup(m => m.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns<string, string, string>((toEmailAddress, subject, body) =>
    //        {
    //            isSmtpEmailHandled = true;

    //            return Task.CompletedTask;
    //        });
    //    });

    //    //act
    //    await EmailService.SendConfirmationEmailAsync(dbUser.Id, emailConfirmationToken);

    //    //assert
    //    ((bool)isSmtpEmailHandled).Should().BeTrue();
    //}
}