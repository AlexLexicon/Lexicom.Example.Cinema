using Lexicom.Example.Cinema.Shared.Models;
using Lexicom.Example.Cinema.Shared.Options;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace Lexicom.Example.Cinema.Shared.Services;

public interface IDataService
{
    Task<IReadOnlyList<MovieData>?> GetAllMovieDataAsync();
}
public class DataService : IDataService
{
    private readonly IOptions<DataOptions> _dataOptions;

    public DataService(IOptions<DataOptions> dataOptions)
    {
        _dataOptions = dataOptions;
    }

    public async Task<IReadOnlyList<MovieData>?> GetAllMovieDataAsync()
    {
        string filePath = Path.GetFullPath(_dataOptions.Value.MoviesJsonFilePath);

        string json = await File.ReadAllTextAsync(filePath);

        return JsonSerializer.Deserialize<List<MovieData>>(json);
    }
}
