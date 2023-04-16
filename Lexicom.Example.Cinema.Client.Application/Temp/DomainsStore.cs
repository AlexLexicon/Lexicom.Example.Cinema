using System.Text.Json;

namespace Lexicom.Example.Cinema.Client.Application.Temp;
public interface IDomainsStore
{
    IReadOnlyList<MovieStore> Movies { get; }
    IReadOnlyList<DirectorStore> Directors { get; }
    IReadOnlyList<ActorStore> Actors { get; }
}
public class DomainsStore : IDomainsStore
{
    public DomainsStore()
    {
        string moviesString = File.ReadAllText(@"C:\Users\alexr\source\repos\Lexicom.Example.Cinema\movies.json");

        var ms = JsonSerializer.Deserialize<List<MovieStore>>(moviesString)!;

        var movies = new List<MovieStore>();
        for (int dup = 0; dup < 1; dup++)
        {
            movies.AddRange(ms);
        }
        Movies = movies;

        foreach (var movie in Movies)
        {
            movie.Id = Guid.NewGuid();
        }

        Directors = new List<DirectorStore>
        {
            new DirectorStore
            {
                Id = Guid.NewGuid(),
                Name = "George Lucas",
            },
            new DirectorStore
            {
                Id = Guid.NewGuid(),
                Name = "Michael Curtiz",
            },
            new DirectorStore
            {
                Id = Guid.NewGuid(),
                Name = "Francis Ford Coppola",
            },
        };

        Actors = new List<ActorStore>
        {
            new ActorStore
            {
                Id = Guid.NewGuid(),
                Name = "Harrison Ford",
            },
            new ActorStore
            {
                Id = Guid.NewGuid(),
                Name = "Al Pacino",
            },
            new ActorStore
            {
                Id = Guid.NewGuid(),
                Name = "Samuel L. Jackson",
            },
        };

        //Movies = Movies.Take(1).ToList();
        //Directors = Directors.Take(1).ToList();
        //Actors = Actors.Take(1).ToList();
    }
    public IReadOnlyList<MovieStore> Movies { get; }
    public IReadOnlyList<DirectorStore> Directors { get; }
    public IReadOnlyList<ActorStore> Actors { get; }
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
