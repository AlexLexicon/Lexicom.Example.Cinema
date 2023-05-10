using Lexicom.AspNetCore.Controllers.Amenities;
using Lexicom.EntityFramework.Amenities;
using Lexicom.Example.Cinema.Server.Movies.Api.Contracts;
using Lexicom.Example.Cinema.Server.Movies.Application.Models;
using Lexicom.Example.Cinema.Server.Movies.Application.Services;
using Lexicom.Swashbuckle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.Example.Cinema.Server.Movies.Api.Controllers;
[Authorize]
[ApiController]
[Route("api/movies/search")]
public class SearchController : LexicomController
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    [SwaggerParameter(SearchGetRequestQuery.QUERY_STRING_OFFSET, 0)]
    [SwaggerParameter(SearchGetRequestQuery.QUERY_STRING_LIMIT, 20)]
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> SearchAsync(
        [FromQuery(Name = SearchGetRequestQuery.QUERY_STRING_OFFSET)] int offset,
        [FromQuery(Name = SearchGetRequestQuery.QUERY_STRING_LIMIT)] int limit,
        [FromQuery(Name = SearchGetRequestQuery.QUERY_STRING_SEARCH_TEXT)] string? searchText)
    {
        //validate query string parameters

        Slice<Movie> movies = await _searchService.SearchMoviesAsync(offset, limit, searchText);

        return Ok(new SearchGetResponseBody
        {
            TotalMovies = movies.TotalCount,
            Movies = movies.Select(m => new SearchGetResponseBodyMovie
            {
                Id = m.Id,
                Title = m.Title,
                Duration = m.Duration,
                ReleaseDateTimeOffsetUtc = m.ReleaseDateTimeOffsetUtc,
            }).ToList(),
        });
    }
}
