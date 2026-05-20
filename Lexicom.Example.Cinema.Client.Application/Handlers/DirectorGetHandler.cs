using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Temp;

namespace Lexicom.Example.Cinema.Client.Application.Handlers;
public class DirectorGetHandler
{
    private readonly IDomainsStore _domainsStore;

    public DirectorGetHandler(IDomainsStore domainsStore)
    {
        _domainsStore = domainsStore;
    }

    public async Task<DirectorGetResponse> Handle(DirectorGetRequest request, CancellationToken cancellationToken)
    {
        var director = _domainsStore.Directors.First(d => d.Id == request.DirectorId);

        await Task.Delay(500, cancellationToken);

        return new DirectorGetResponse(director.Name);
    }
}
