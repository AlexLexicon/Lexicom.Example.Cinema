using Lexicom.Example.Cinema.Client.Application.Models;

namespace Lexicom.Example.Cinema.Client.Application.Services;

public interface IDomainService
{
    Task<IReadOnlyList<Domain>> GetDomainsAsync();
}
public class DomainService : IDomainService
{
    private IReadOnlyList<Domain>? OrderedDomains { get; set; }

    public Task<IReadOnlyList<Domain>> GetDomainsAsync()
    {
        OrderedDomains ??= Enum
            .GetValues<Domain>()
            .OrderByDescending(d => d)
            .ToList();

        return Task.FromResult(OrderedDomains);
    }
}
