using Lexicom.Example.Cinema.Client.Core.Mediator;
using Lexicom.Example.Cinema.Client.Core.Temp;
using MediatR;

namespace Lexicom.Example.Cinema.Client.Core.Handlers;
public class DirectorGetHandler : IRequestHandler<DirectorGetRequest, DirectorGetResponse>
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
