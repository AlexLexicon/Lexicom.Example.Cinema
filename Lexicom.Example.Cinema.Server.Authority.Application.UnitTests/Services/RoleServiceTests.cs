namespace Lexicom.Example.Cinema.Server.Authority.Application.UnitTests.Services;
public class RoleServiceTests
{
    //private readonly UnitTestAttendant _test;
    //private IRoleService RoleService => _test.Get<IRoleService>();
    //private RoleManager<Role> RoleManager => _test.Get<RoleManager<Role>>();
    //private AuthorityDbContext Db => _test.Get<AuthorityDbContext>();

    //public RoleServiceTests()
    //{
    //    _test = new UnitTestAttendant();

    //    _test.AddTestAuthorityEntityFrameworkAndIdentity();

    //    _test.AddLogging();
    //    _test.AddTestProviders();

    //    _test.Mock<IPermissionService>();

    //    _test.AddSingleton<IRoleService, RoleService>();
    //}

    //[Fact]
    //public async Task GetRoleByIdAsync_Returns_Role()
    //{
    //    //arrange
    //    Role dbRole = await RoleManager.CreateAsync();

    //    //act
    //    Role role = await RoleService.GetRoleByIdAsync(dbRole.Id);

    //    //assert
    //    role.Should().NotBeNull();
    //    role.Should().BeEquivalentTo(dbRole);
    //}

    //[Fact]
    //public async Task GetRoleByIdAsync_Throws_RoleDoesNotExistException()
    //{
    //    //arrange
    //    Guid roleId = Guid.NewGuid();

    //    //assert
    //    await Assert.ThrowsAsync<RoleDoesNotExistException>(async () =>
    //    {
    //        //act
    //        await RoleService.GetRoleByIdAsync(roleId);
    //    });
    //}

    //[Fact]
    //public async Task GetRoleByNameAsync_Returns_Role()
    //{
    //    //arrange
    //    Role dbRole = await RoleManager.CreateAsync();

    //    //act
    //    Role role = await RoleService.GetRoleByNameAsync(dbRole.Name);

    //    //assert
    //    role.Should().NotBeNull();
    //    role.Should().BeEquivalentTo(dbRole);
    //}

    //[Fact]
    //public async Task GetRoleByNameAsync_Throws_RoleDoesNotExistException()
    //{
    //    //arrange
    //    string name = "role_does_not_exist_exception_role";

    //    //assert
    //    await Assert.ThrowsAsync<RoleDoesNotExistException>(async () =>
    //    {
    //        //act
    //        await RoleService.GetRoleByNameAsync(name);
    //    });
    //}

    //[Fact]
    //public async Task GetRolePermissionsAsync_Returns_Permissions()
    //{
    //    //arrange
    //    Role dbRole = await RoleManager.CreateAsync();

    //    var dbPermissions = new string[3];
    //    dbPermissions[0] = await RoleManager.AddPermissionAsync(dbRole);
    //    dbPermissions[1] = await RoleManager.AddPermissionAsync(dbRole);
    //    dbPermissions[2] = await RoleManager.AddPermissionAsync(dbRole);

    //    //act
    //    IReadOnlyList<string> permissions = await RoleService.GetRolePermissionsAsync(dbRole.Id);

    //    permissions = permissions.OrderBy(p => p).ToArray();
    //    dbPermissions = dbPermissions.OrderBy(p => p).ToArray();

    //    //assert
    //    permissions.Should().HaveCount(3);
    //    permissions[0].Should().Be(dbPermissions[0]);
    //    permissions[1].Should().Be(dbPermissions[1]);
    //    permissions[2].Should().Be(dbPermissions[2]);
    //}

    //[Fact]
    //public async Task GetRolePermissionsAsync_Throws_RoleDoesNotExistException()
    //{
    //    //arrange
    //    Guid roleId = Guid.NewGuid();

    //    //assert
    //    await Assert.ThrowsAsync<RoleDoesNotExistException>(async () =>
    //    {
    //        //act
    //        await RoleService.GetRolePermissionsAsync(roleId);
    //    });
    //}

    //[Fact]
    //public async Task CreateRoleAsync_Returns_Role()
    //{
    //    //arrange
    //    string name = "testrole";

    //    //act
    //    Role role = await RoleService.CreateRoleAsync(name);

    //    //assert
    //    role.Should().NotBeNull();
    //    role.Name.Should().Be(name);
    //}

    //[Theory]
    //[InlineData("rolename", "rolename")]
    //[InlineData("rolename", "ROLENAME")]
    //[InlineData("rolename", "Rolename")]
    //[InlineData("rolename", "rOlEnAmE")]
    //public async Task CreateRoleAsync_Throws_RoleAlreadyExistsException(string name, string newName)
    //{
    //    //arrange
    //    Role dbRole = await RoleManager.CreateAsync(name: name);

    //    //assert
    //    await Assert.ThrowsAsync<RoleAlreadyExistsException>(async () =>
    //    {
    //        //act
    //        await RoleService.CreateRoleAsync(newName);
    //    });
    //}

    //[Theory]
    //[InlineData("")]
    //[InlineData(" ")]
    //[InlineData("\t")]
    //[InlineData("\n")]
    //[InlineData("\r")]
    //public async Task CreateRoleAsync_Throws_InvalidRoleNameException(string name)
    //{
    //    //arrange
    //    Guid roleId = Guid.NewGuid();

    //    //assert
    //    await Assert.ThrowsAsync<RoleNameNotValidException>(async () =>
    //    {
    //        //act
    //        await RoleService.CreateRoleAsync(name);
    //    });
    //}

    //[Fact]
    //public async Task UpdateRoleAsync_NewName()
    //{
    //    //arrange
    //    string originalName = "oringal_name_role";
    //    string? newName = "new_name_role";

    //    Role dbRole = await RoleManager.CreateAsync(originalName);

    //    //act
    //    await RoleService.UpdateRoleAsync(dbRole.Id, newName);

    //    //assert
    //    Role role = await Db.Roles.SingleAsync(r => r.Id == dbRole.Id);

    //    role.Name.Should().NotBe(originalName);
    //    role.Name.Should().Be(newName);
    //}

    //[Fact]
    //public async Task UpdateRoleAsync_NewName_Same()
    //{
    //    //arrange
    //    string originalName = "oringal_name_role";
    //    string? newName = originalName;

    //    Role dbRole = await RoleManager.CreateAsync(originalName);

    //    //act
    //    await RoleService.UpdateRoleAsync(dbRole.Id, newName);

    //    //assert
    //    Role role = await Db.Roles.SingleAsync(r => r.Id == dbRole.Id);

    //    role.Name.Should().Be(originalName);
    //}

    //[Fact]
    //public async Task UpdateRoleAsync_NewName_Null()
    //{
    //    //arrange
    //    string originalName = "oringal_name_role";
    //    string? newName = null;

    //    Role dbRole = await RoleManager.CreateAsync(originalName);

    //    //act
    //    await RoleService.UpdateRoleAsync(dbRole.Id, newName);

    //    //assert
    //    Role role = await Db.Roles.SingleAsync(r => r.Id == dbRole.Id);

    //    role.Name.Should().NotBeNull();
    //    role.Name.Should().Be(originalName);
    //}

    //[Fact]
    //public async Task UpdateRoleAsync_Throws_RoleDoesNotExistException()
    //{
    //    //arrange
    //    Guid roleId = Guid.NewGuid();
    //    string? newName = "role_does_not_exist_exception_role";

    //    //assert
    //    await Assert.ThrowsAsync<RoleDoesNotExistException>(async () =>
    //    {
    //        //act
    //        await RoleService.UpdateRoleAsync(roleId, newName);
    //    });
    //}

    //[Fact]
    //public async Task UpdateRoleAsync_Throws_RoleNameAlreadyInUseException()
    //{
    //    //arrange
    //    string otherName = "role_name_already_in_use_exception_role";
    //    string? newName = otherName;

    //    Role dbRole = await RoleManager.CreateAsync();
    //    Role dbOtherRole = await RoleManager.CreateAsync(otherName);

    //    //assert
    //    await Assert.ThrowsAsync<RoleNameAlreadyInUseException>(async () =>
    //    {
    //        //act
    //        await RoleService.UpdateRoleAsync(dbRole.Id, newName);
    //    });
    //}

    //[Fact]
    //public async Task AddPermissionToRoleAsync()
    //{
    //    //arrange
    //    string permission = "testpermission";

    //    Role dbRole = await RoleManager.CreateAsync();

    //    _test.Mock<IPermissionService>(m =>
    //    {
    //        m.Setup(m => m.PermissionExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
    //    });

    //    //act
    //    await RoleService.AddPermissionToRoleAsync(dbRole.Id, permission);

    //    //assert
    //    List<string> permissions = await RoleManager.GetPermissionsAsync(dbRole);

    //    permissions.Should().HaveCount(1);
    //    permissions[0].Should().Be(permission);
    //}

    //[Fact]
    //public async Task AddPermissionToRoleAsync_Throws_PermissionDoesNotExistException()
    //{
    //    //arrange
    //    string permission = "testpermission";

    //    Role dbRole = await RoleManager.CreateAsync();

    //    _test.Mock<IPermissionService>(m =>
    //    {
    //        m.Setup(m => m.PermissionExistsAsync(It.IsAny<string>())).ReturnsAsync(false);
    //    });

    //    //assert
    //    await Assert.ThrowsAsync<PermissionDoesNotExistException>(async () =>
    //    {
    //        //act
    //        await RoleService.AddPermissionToRoleAsync(dbRole.Id, permission);
    //    });
    //}

    //[Fact]
    //public async Task AddPermissionToRoleAsync_Throws_RoleDoesNotExistException()
    //{
    //    //arrange
    //    Guid roleId = Guid.NewGuid();
    //    string permission = "testpermission";

    //    _test.Mock<IPermissionService>(m =>
    //    {
    //        m.Setup(m => m.PermissionExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
    //    });

    //    //assert
    //    await Assert.ThrowsAsync<RoleDoesNotExistException>(async () =>
    //    {
    //        //act
    //        await RoleService.AddPermissionToRoleAsync(roleId, permission);
    //    });
    //}

    //[Fact]
    //public async Task AddPermissionToRoleAsync_Throws_RoleAlreadyHasPermissionException()
    //{
    //    //arrange
    //    Role dbRole = await RoleManager.CreateAsync();

    //    string permission = await RoleManager.AddPermissionAsync(dbRole);

    //    _test.Mock<IPermissionService>(m =>
    //    {
    //        m.Setup(m => m.PermissionExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
    //    });

    //    //assert
    //    await Assert.ThrowsAsync<RoleAlreadyHasPermissionException>(async () =>
    //    {
    //        //act
    //        await RoleService.AddPermissionToRoleAsync(dbRole.Id, permission);
    //    });
    //}

    //[Fact]
    //public async Task RemovePermissionFromRoleAsync()
    //{
    //    //arrange
    //    Role dbRole = await RoleManager.CreateAsync();

    //    string permission = await RoleManager.AddPermissionAsync(dbRole);

    //    //act
    //    await RoleService.RemovePermissionFromRoleAsync(dbRole.Id, permission);

    //    //assert
    //    List<string> permissions = await RoleManager.GetPermissionsAsync(dbRole);

    //    permissions.Should().BeEmpty();
    //}

    //[Fact]
    //public async Task RemovePermissionFromRoleAsync_Throws_RoleDoesNotExistException()
    //{
    //    //arrange
    //    Guid roleId = Guid.NewGuid();
    //    string permission = "testpermission";

    //    //assert
    //    await Assert.ThrowsAsync<RoleDoesNotExistException>(async () =>
    //    {
    //        //act
    //        await RoleService.RemovePermissionFromRoleAsync(roleId, permission);
    //    });
    //}

    //[Fact]
    //public async Task RemovePermissionFromRoleAsync_Throws_RoleDoesNotHavePermissionException()
    //{
    //    //arrange
    //    string permission = "testpermission";

    //    Role dbRole = await RoleManager.CreateAsync();

    //    //assert
    //    await Assert.ThrowsAsync<RoleDoesNotHavePermissionException>(async () =>
    //    {
    //        //act
    //        await RoleService.RemovePermissionFromRoleAsync(dbRole.Id, permission);
    //    });
    //}
}
