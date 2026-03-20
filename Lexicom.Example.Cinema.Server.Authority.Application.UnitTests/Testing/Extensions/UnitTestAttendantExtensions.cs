using Lexicom.EntityFramework.Identity.UnitTesting.Extensions;
using Lexicom.EntityFramework.UnitTesting.Extensions;
using Lexicom.Example.Cinema.Server.Authority.Database;
using Lexicom.Example.Cinema.Server.Authority.Database.Entities;
using Lexicom.UnitTesting;

namespace Lexicom.Example.Cinema.Server.Authority.Application.UnitTests.Testing.Extensions;

public static class UnitTestAttendantExtensions
{
    public static void AddTestAuthorityEntityFrameworkAndIdentity(this UnitTestAttendant unitTestAttendant)
    {
        unitTestAttendant.AddTestEntityFramework(options =>
        {
            options.AddTestDataContext<AuthorityDbContext>();
            options.AddTestIdentity<AuthorityDbContext, User, Role, Guid>(Constants.IDENTITYCONFIGURATION);
        });
    }
}
