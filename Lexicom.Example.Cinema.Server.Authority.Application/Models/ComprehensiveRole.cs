namespace Lexicom.Example.Cinema.Server.Authority.Application.Models;
public class ComprehensiveRole
{
    public required Guid Id { get; init; }
    public required string Name { get; init; }
    public required IReadOnlyList<string> Permissions { get; init; }
}
