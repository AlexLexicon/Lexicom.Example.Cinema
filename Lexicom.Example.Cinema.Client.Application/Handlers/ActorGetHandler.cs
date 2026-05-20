using Lexicom.Example.Cinema.Client.Application.Mediator;
using Lexicom.Example.Cinema.Client.Application.Temp;

namespace Lexicom.Example.Cinema.Client.Application.Handlers;
public class ActorGetHandler
{
    private readonly IDomainsStore _domainsStore;

    public ActorGetHandler(IDomainsStore domainsStore)
    {
        _domainsStore = domainsStore;
    }

    public async Task<ActorGetResponse> Handle(ActorGetRequest request, CancellationToken cancellationToken)
    {
        var actor = _domainsStore.Actors.First(a => a.Id == request.ActorId);

        await Task.Delay(500, cancellationToken);

        return new ActorGetResponse(actor.Name);
    }
}
