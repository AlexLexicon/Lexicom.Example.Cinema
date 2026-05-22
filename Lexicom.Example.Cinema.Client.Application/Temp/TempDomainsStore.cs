//using Lexicom.Example.Cinema.Client.Application.Models;
//using Lexicom.Example.Cinema.Shared.Services;

//namespace Lexicom.Example.Cinema.Client.Application.Temp;

//public interface IDomainsStore
//{
//    Task LoadAsync();
//    IReadOnlyList<Movie> Movies { get; }
//    IReadOnlyList<Director> Directors { get; }
//    IReadOnlyList<Actor> Actors { get; }
//}
//public class DomainsStore : IDomainsStore
//{
//    private readonly IDataService _dataService;

//    public DomainsStore(IDataService dataService)
//    {
//        _dataService = dataService;

//        MoviesStore = [];
//        DirectorsStore = [];
//        ActorsStore = [];
//    }


//    private List<Director> DirectorsStore { get; set; }
//    private List<Actor> ActorsStore { get; set; }

//    public IReadOnlyList<Movie> Movies => MoviesStore;
//    public IReadOnlyList<Director> Directors => DirectorsStore;
//    public IReadOnlyList<Actor> Actors => ActorsStore;

//    public async Task LoadAsync()
//    {
//        var movieData = await _dataService.GetAllMovieDataAsync();

//        if (movieData is not null)
//        {
//            foreach (var data in movieData)
//            {
//                MoviesStore.Add(new Movie
//                {
//                    Id = Guid.NewGuid(),
//                    Title = data.Title,
//                    Duration = data.Duration,
//                    Synopsis = data.Synopsis,
//                    ReleaseDateTimeOffsetUtc = data.ReleaseDateTimeOffsetUtc,
//                });
//            }

//        }

//        DirectorsStore.Add(new Director
//        {
//            Id = Guid.NewGuid(),
//            Name = "George Lucas",
//        });
//        DirectorsStore.Add(new Director
//        {
//            Id = Guid.NewGuid(),
//            Name = "Michael Curtiz",
//        });
//        DirectorsStore.Add(new Director
//        {
//            Id = Guid.NewGuid(),
//            Name = "Francis Ford Coppola",
//        });

//        ActorsStore.Add(new Actor
//        {
//            Id = Guid.NewGuid(),
//            Name = "Harrison Ford",
//        });
//        ActorsStore.Add(new Actor
//        {
//            Id = Guid.NewGuid(),
//            Name = "Al Pacino",
//        });
//        ActorsStore.Add(new Actor
//        {
//            Id = Guid.NewGuid(),
//            Name = "Samuel L. Jackson",
//        });
//    }
//}
