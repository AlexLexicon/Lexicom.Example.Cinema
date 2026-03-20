using Lexicom.Example.Cinema.Shared.Services;

namespace Lexicom.Example.Cinema.Client.Application.Temp;

public interface IDomainsStore
{
    Task LoadAsync();
    IReadOnlyList<MovieStore> Movies { get; }
    IReadOnlyList<DirectorStore> Directors { get; }
    IReadOnlyList<ActorStore> Actors { get; }
}
public class DomainsStore : IDomainsStore
{
    private readonly IDataService _dataService;

    public DomainsStore(IDataService dataService)
    {
        _dataService = dataService;

        MoviesStore = [];
        DirectorsStore = [];
        ActorsStore = [];
    }

    private List<MovieStore> MoviesStore { get; set; }
    private List<DirectorStore> DirectorsStore { get; set; }
    private List<ActorStore> ActorsStore { get; set; }

    public IReadOnlyList<MovieStore> Movies => MoviesStore;
    public IReadOnlyList<DirectorStore> Directors => DirectorsStore;
    public IReadOnlyList<ActorStore> Actors => ActorsStore;

    public async Task LoadAsync()
    {
        var movieData = await _dataService.GetAllMovieDataAsync();

        if (movieData is not null)
        {
            foreach (var data in movieData)
            {
                MoviesStore.Add(new MovieStore
                {
                    Id = Guid.NewGuid(),
                    Title = data.Title,
                    Duration = data.Duration,
                    Synopsis = data.Synopsis,
                    ReleaseDateTimeOffsetUtc = data.ReleaseDateTimeOffsetUtc,
                });
            }

        }

        DirectorsStore.Add(new DirectorStore
        {
            Id = Guid.NewGuid(),
            Name = "George Lucas",
        });
        DirectorsStore.Add(new DirectorStore
        {
            Id = Guid.NewGuid(),
            Name = "Michael Curtiz",
        });
        DirectorsStore.Add(new DirectorStore
        {
            Id = Guid.NewGuid(),
            Name = "Francis Ford Coppola",
        });

        ActorsStore.Add(new ActorStore
        {
            Id = Guid.NewGuid(),
            Name = "Harrison Ford",
        });
        ActorsStore.Add(new ActorStore
        {
            Id = Guid.NewGuid(),
            Name = "Al Pacino",
        });
        ActorsStore.Add(new ActorStore
        {
            Id = Guid.NewGuid(),
            Name = "Samuel L. Jackson",
        });
    }
}
public class MovieStore
{
    public Guid Id { get; set; }
    public required string Title { get; set; }
    public required TimeSpan Duration { get; set; }
    public required DateTimeOffset ReleaseDateTimeOffsetUtc { get; set; }
    public required string Synopsis { get; set; }
}
public class DirectorStore
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}
public class ActorStore
{
    public Guid Id { get; set; }
    public required string Name { get; set; }
}
