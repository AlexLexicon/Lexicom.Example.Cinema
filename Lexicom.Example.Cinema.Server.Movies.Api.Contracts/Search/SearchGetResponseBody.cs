namespace Lexicom.Example.Cinema.Server.Movies.Api.Contracts.Search;
public class SearchGetResponseBody
{
    public required int TotalMovies { get; set; }
    public required List<SearchGetResponseBodyMovie> Movies { get; set; }
}
