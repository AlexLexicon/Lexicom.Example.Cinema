namespace Lexicom.Example.Cinema.Server.Authority.Application.UnitTests.Services;
public class UserServiceTests
{
    //private readonly UnitTestAttendant _test;
    //private IUserService UserService => _test.Get<IUserService>();
    //private UserManager<User> UserManager => _test.Get<UserManager<User>>();
    //private RoleManager<Role> RoleManager => _test.Get<RoleManager<Role>>();
    //private AuthorityDbContext Db => _test.Get<AuthorityDbContext>();

    //public UserServiceTests()
    //{
    //    _test = new UnitTestAttendant();

    //    _test.AddTestAuthorityEntityFrameworkAndIdentity();

    //    _test.AddLogging();
    //    _test.AddTestProviders();

    //    _test.Mock<IRoleService>();

    //    _test.AddSingleton<IUserService, UserService>();
    //}

    //[Fact]
    //public async Task GetUserByIdAsync_Returns_User()
    //{
    //    //arrange
    //    User dbUser = await UserManager.CreateAsync();

    //    //act
    //    User user = await UserService.GetUserByIdAsync(dbUser.Id);

    //    //assert
    //    user.Should().NotBeNull();
    //    user.Should().BeEquivalentTo(dbUser);
    //}

    //[Fact]
    //public async Task GetUserByIdAsync_Throws_UserDoesNotExistException()
    //{
    //    //arrange
    //    Guid id = Guid.NewGuid();

    //    //assert
    //    await Assert.ThrowsAsync<UserDoesNotExistException>(async () =>
    //    {
    //        //act
    //        await UserService.GetUserByIdAsync(id);
    //    });
    //}

    //[Fact]
    //public async Task GetUserByEmailAsync_Returns_User()
    //{
    //    //arrange
    //    User dbUser = await UserManager.CreateAsync();

    //    //act
    //    User user = await UserService.GetUserByEmailAsync(dbUser.Email);

    //    //assert
    //    user.Should().NotBeNull();
    //    user.Should().BeEquivalentTo(dbUser);
    //}

    //[Fact]
    //public async Task GetUserByEmailAsync_Throws_UserDoesNotExistException()
    //{
    //    //arrange
    //    string email = "user_does_not_exist_exception@email.com";

    //    //assert
    //    await Assert.ThrowsAsync<UserDoesNotExistException>(async () =>
    //    {
    //        //act
    //        await UserService.GetUserByEmailAsync(email);
    //    });
    //}

    //[Fact]
    //public async Task GetUserRolesAsync_Returns_Roles()
    //{
    //    //arrange
    //    User dbUser = await UserManager.CreateAsync();

    //    var dbRoles = new Role[3];
    //    dbRoles[0] = await RoleManager.CreateAsync();
    //    dbRoles[1] = await RoleManager.CreateAsync();
    //    dbRoles[2] = await RoleManager.CreateAsync();

    //    await UserManager.AddToRoleAsync(dbUser, dbRoles[0].Name);
    //    await UserManager.AddToRoleAsync(dbUser, dbRoles[1].Name);
    //    await UserManager.AddToRoleAsync(dbUser, dbRoles[2].Name);

    //    //act
    //    await UserService.GetUserRolesAsync(dbUser.Id);

    //    //assert
    //    List<Role> roles = await Db.GetRolesAsync(dbUser);

    //    roles = roles.OrderBy(r => r.Id).ToList();
    //    dbRoles = dbRoles.OrderBy(r => r.Id).ToArray();

    //    roles.Should().HaveCount(3);
    //    roles[0].Should().BeEquivalentTo(dbRoles[0]);
    //    roles[1].Should().BeEquivalentTo(dbRoles[1]);
    //    roles[2].Should().BeEquivalentTo(dbRoles[2]);
    //}

    //[Fact]
    //public async Task GetUserRolesAsync_Throws_UserDoesNotExistException()
    //{
    //    //arrange
    //    Guid userId = Guid.NewGuid();

    //    //assert
    //    await Assert.ThrowsAsync<UserDoesNotExistException>(async () =>
    //    {
    //        //act
    //        await UserService.GetUserRolesAsync(userId);
    //    });
    //}

    //[Fact]
    //public async Task CreateUserAsync_Returns_User()
    //{
    //    //arrange
    //    string email = "create_user@email.com";
    //    string password = "Password1234!";
    //    string firstName = "testFirstName";
    //    string lastName = "testLastName";

    //    //act
    //    User user = await UserService.CreateUserAsync(email, password, firstName, lastName);

    //    //assert
    //    user.Should().NotBeNull();

    //    User dbUser = await Db.Users.SingleAsync();

    //    user.Email.Should().Be(email);
    //    user.NormalizedEmail.Should().Be(email.ToUpperInvariant());
    //    user.UserName.Should().Be(user.Id.ToString());
    //    user.NormalizedUserName.Should().Be(user.Id.ToString().ToUpperInvariant());
    //    user.FirstName.Should().Be(firstName);
    //    user.LastName.Should().Be(lastName);

    //    user.Should().BeEquivalentTo(dbUser);
    //}

    //[Theory]
    //[InlineData("test@email.com", "test@email.com")]
    //[InlineData("test@email.com", "TEST@EMAIL.COM")]
    //[InlineData("test@email.com", "Test@email.com")]
    //[InlineData("test@email.com", "test@Email.com")]
    //[InlineData("test@email.com", "test@email.Com")]
    //[InlineData("test@email.com", "TeSt@eMaIl.CoM")]
    //public async Task CreateUserAsync_Throws_EmailAlreadyInUseException(string email, string newEmail)
    //{
    //    //arrange
    //    string password = "Password1234!";
    //    string firstName = "testFirstName";
    //    string lastName = "testLastName";

    //    User dbUser = await UserManager.CreateAsync(email: email);

    //    //assert
    //    await Assert.ThrowsAsync<EmailAlreadyInUseException>(async () =>
    //    {
    //        //act
    //        await UserService.CreateUserAsync(newEmail, password, firstName, lastName);
    //    });
    //}

    //[Fact]
    //public async Task CreateUserAsync_Throws_EmailNotValidException()
    //{
    //    //arrange
    //    string email = "email_not_valid_exception";
    //    string password = "Password1234!";
    //    string firstName = "testFirstName";
    //    string lastName = "testLastName";

    //    //assert
    //    await Assert.ThrowsAsync<EmailNotValidException>(async () =>
    //    {
    //        //act
    //        await UserService.CreateUserAsync(email, password, firstName, lastName);
    //    });
    //}

    //[Fact]
    //public async Task CreateUserAsync_Throws_PasswordMissingRequirementsException()
    //{
    //    //arrange
    //    string email = "password_missing_requirements_exception@email.com";
    //    string password = "abc";
    //    string firstName = "testFirstName";
    //    string lastName = "testLastName";

    //    //assert
    //    await Assert.ThrowsAsync<PasswordMissingRequirementsException>(async () =>
    //    {
    //        //act
    //        await UserService.CreateUserAsync(email, password, firstName, lastName);
    //    });
    //}

    //[Fact]
    //public async Task AddRoleToUserAsync()
    //{
    //    //arrange
    //    User dbUser = await UserManager.CreateAsync();
    //    Role dbRole = await RoleManager.CreateAsync();

    //    _test.Mock<IRoleService>(m =>
    //    {
    //        m.Setup(m => m.GetRoleByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dbRole);
    //    });

    //    //act
    //    await UserService.AddRoleToUserAsync(dbUser.Id, dbRole.Id);

    //    //assert
    //    Role userDbRole = (await Db.GetRolesAsync(dbUser)).Single();

    //    userDbRole.Should().BeEquivalentTo(dbRole);
    //}

    //[Fact]
    //public async Task AddRoleToUserAsync_Throws_UserDoesNotExistException()
    //{
    //    //arrange
    //    Guid userId = Guid.NewGuid();
    //    Guid roleId = Guid.NewGuid();

    //    //assert
    //    await Assert.ThrowsAsync<UserDoesNotExistException>(async () =>
    //    {
    //        //act
    //        await UserService.AddRoleToUserAsync(userId, roleId);
    //    });
    //}

    //[Fact]
    //public async Task AddRoleToUserAsync_Throws_UserAlreadyHasRoleException()
    //{
    //    //arrange
    //    User dbUser = await UserManager.CreateAsync();
    //    Role dbRole = await RoleManager.CreateAsync();

    //    _test.Mock<IRoleService>(m =>
    //    {
    //        m.Setup(m => m.GetRoleByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dbRole);
    //    });

    //    await UserManager.AddToRoleAsync(dbUser, dbRole.Name);

    //    //assert
    //    await Assert.ThrowsAsync<UserAlreadyHasRoleException>(async () =>
    //    {
    //        //act
    //        await UserService.AddRoleToUserAsync(dbUser.Id, dbRole.Id);
    //    });
    //}

    //[Fact]
    //public async Task RemoveRoleFromUserAsync()
    //{
    //    //arrange
    //    User dbUser = await UserManager.CreateAsync();
    //    Role dbRole = await RoleManager.CreateAsync();

    //    _test.Mock<IRoleService>(m =>
    //    {
    //        m.Setup(m => m.GetRoleByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dbRole);
    //    });

    //    await UserManager.AddToRoleAsync(dbUser, dbRole.Name);

    //    //act
    //    await UserService.RemoveRoleFromUserAsync(dbUser.Id, dbRole.Id);

    //    //assert
    //    bool userHasRoles = await Db.UserRoles.AnyAsync(ur => ur.UserId == dbUser.Id);

    //    userHasRoles.Should().BeFalse();
    //}

    //[Fact]
    //public async Task RemoveRoleFromUserAsync_Throws_UserDoesNotExistException()
    //{
    //    //arrange
    //    Guid userId = Guid.NewGuid();
    //    Guid roleId = Guid.NewGuid();

    //    //assert
    //    await Assert.ThrowsAsync<UserDoesNotExistException>(async () =>
    //    {
    //        //act
    //        await UserService.RemoveRoleFromUserAsync(userId, roleId);
    //    });
    //}

    //[Fact]
    //public async Task RemoveRoleFromUserAsync_Throws_UserDoesNotHaveRoleException()
    //{
    //    //arrange
    //    User dbUser = await UserManager.CreateAsync();
    //    Role dbRole = await RoleManager.CreateAsync();

    //    _test.Mock<IRoleService>(m =>
    //    {
    //        m.Setup(m => m.GetRoleByIdAsync(It.IsAny<Guid>())).ReturnsAsync(dbRole);
    //    });

    //    //assert
    //    await Assert.ThrowsAsync<UserDoesNotHaveRoleException>(async () =>
    //    {
    //        //act
    //        await UserService.RemoveRoleFromUserAsync(dbUser.Id, dbRole.Id);
    //    });
    //}
}
