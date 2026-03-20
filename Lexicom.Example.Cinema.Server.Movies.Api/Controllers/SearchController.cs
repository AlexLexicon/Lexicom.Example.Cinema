using Lexicom.AspNetCore.Controllers.Amenities;
using Lexicom.EntityFramework.Amenities;
using Lexicom.Example.Cinema.Server.Movies.Api.Contracts;
using Lexicom.Example.Cinema.Server.Movies.Application.Services;
using Lexicom.Example.Cinema.Server.Movies.Database.Entities;
using Lexicom.Scalar;
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

    [ScalarDefaultParameter(SearchGetRequestQuery.QUERY_STRING_PAGE, 0)]
    [ScalarDefaultParameter(SearchGetRequestQuery.QUERY_STRING_COUNT, 20)]
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> SearchAsync(
        [FromQuery(Name = SearchGetRequestQuery.QUERY_STRING_PAGE)] int offset,
        [FromQuery(Name = SearchGetRequestQuery.QUERY_STRING_COUNT)] int limit,
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
