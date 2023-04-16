namespace Lexicom.Example.Cinema.Server.Authority.Application.UnitTests.Services;
public class ModerationServiceTests
{
    //private readonly UnitTestAttendant _test;
    //private IModerationService ModerationService => _test.Get<IModerationService>();
    //private UserManager<User> UserManager => _test.Get<UserManager<User>>();

    //public ModerationServiceTests()
    //{
    //    _test = new UnitTestAttendant();

    //    _test.AddTestAuthorityEntityFrameworkAndIdentity();

    //    _test.AddLogging();
    //    _test.AddTestProviders();

    //    _test.Mock<IUserService>();

    //    _test.AddSingleton<IModerationService, ModerationService>();
    //}

    //[Fact]
    //public async Task LockUserAsync()
    //{
    //    //arrange
    //    User dbUser = await UserManager.CreateAsync();

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dbUser);
    //    });

    //    //act
    //    await ModerationService.LockUserAsync(dbUser.Id);

    //    //assert
    //    (await UserManager.IsLockedOutAsync(dbUser)).Should().BeTrue();
    //}

    //[Fact]
    //public async Task LockUserAsync_Throws_UserAlreadyLockedOutException()
    //{
    //    //arrange
    //    User dbUser = await UserManager.CreateAsync();

    //    await UserManager.SetLockoutEndDateAsync(dbUser, DateTimeOffset.MaxValue);

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dbUser);
    //    });

    //    //assert
    //    await Assert.ThrowsAsync<UserAlreadyLockedOutException>(async () =>
    //    {
    //        //act
    //        await ModerationService.LockUserAsync(dbUser.Id);
    //    });
    //}

    //[Fact]
    //public async Task UnlockUserAsync()
    //{
    //    //arrange
    //    User dbUser = await UserManager.CreateAsync();

    //    await UserManager.SetLockoutEndDateAsync(dbUser, DateTimeOffset.MaxValue);

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dbUser);
    //    });

    //    //act
    //    await ModerationService.UnlockUserAsync(dbUser.Id);

    //    //assert
    //    (await UserManager.IsLockedOutAsync(dbUser)).Should().BeFalse();
    //}

    //[Fact]
    //public async Task UnlockUserAsync_Throws_UserNotLockedOutException()
    //{
    //    //arrange
    //    User dbUser = await UserManager.CreateAsync();

    //    _test.Mock<IUserService>(m =>
    //    {
    //        m.Setup(m => m.GetUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dbUser);
    //    });

    //    //assert
    //    await Assert.ThrowsAsync<UserNotLockedOutException>(async () =>
    //    {
    //        //act
    //        await ModerationService.UnlockUserAsync(dbUser.Id);
    //    });
    //}
}
