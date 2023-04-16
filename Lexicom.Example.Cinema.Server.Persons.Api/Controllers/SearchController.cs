using Lexicom.AspNetCore.Controllers.Amenities;
using Lexicom.Example.Cinema.Server.Persons.Api.Contracts;
using Lexicom.Example.Cinema.Server.Persons.Application.Services;
using Lexicom.Swashbuckle;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.Example.Cinema.Server.Persons.Api.Controllers;
[Authorize]
[ApiController]
[Route("api/persons/search")]
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

        //Aggregate<Movie> persons = await _searchService.SearchPersonsAsync(offset, limit, searchText);

        return Ok(new SearchGetResponseBody
        {
            TotalPersons = 0,
            Persons = new List<SearchGetResponseBodyPerson>(),
        });
    }
}

