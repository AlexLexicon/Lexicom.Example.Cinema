using Lexicom.Example.Cinema.Server.Authority.Database.Entities;

namespace Lexicom.Example.Cinema.Server.Authority.Application.Models;

public class DecryptedUser : User
{
    public required string DecryptedFirstName { get; init; }
    public required string DecryptedLastName { get; init; }
}
