using Lexicom.AspNetCore.Controllers.Amenities;
using Lexicom.Example.Cinema.Server.Movies.Api.Contracts;
using Lexicom.Example.Cinema.Server.Movies.Application.Exceptions;
using Lexicom.Example.Cinema.Server.Movies.Application.Services;
using Lexicom.Example.Cinema.Server.Movies.Database.Entities;
using Lexicom.Example.Cinema.Server.Shared.Authentication;
using Lexicom.Scalar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.Example.Cinema.Server.Movies.Api.Controllers;

[Authorize]
[ApiController]
[Route("api/movies")]
public class MovieController : LexicomController
{
    private readonly ILogger<MovieController> _logger;
    private readonly IMovieService _movieService;

    public MovieController(
        ILogger<MovieController> logger,
        IMovieService movieService)
    {
        _logger = logger;
        _movieService = movieService;
    }

    [HttpGet("{movieId}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetMovieAsync([FromRoute] Guid movieId)
    {
        try
        {
            Movie movie = await _movieService.GetMovieAsync(movieId);

            return Ok(new MovieGetResponseBody
            {
                Id = movie.Id,
                Title = movie.Title,
                Duration = movie.Duration,
                ReleaseDateTimeOffsetUtc = movie.ReleaseDateTimeOffsetUtc,
                Synopsis = movie.Synopsis,
            });
        }
        catch (MovieDoesNotExistException e)
        {
            _logger.LogWarning(e, "Failed to get the movie with the id '{movieId}' because it does not exist.", movieId);

            return NotFound();
        }
    }

    [ScalarDefaultRequestBody("""
    {
        "Title": "My Example Movie",
        "Duration": "01:23:45",
        "ReleaseDateTimeOffsetUtc": "2000-01-23T12:34:56Z",
        "Synopsis": "This is an example for the purposes of testing."
    }
    """)]
    [HttpPost]
    [Authorize(Policy = Policies.Permissions.Movies.Movie.POST)]
    public async Task<IActionResult> CreateMovieAsync([FromBody] MoviePostRequestBody requestBody)
    {
        try
        {
            Movie movie = await _movieService.CreateMovieAsync(requestBody.Title, requestBody.Duration, requestBody.ReleaseDateTimeOffsetUtc, requestBody.Synopsis);

            return Ok(new MoviePostResponseBody
            {
                CreatedMovieId = movie.Id
            });
        }
        catch (TitleAlreadyInUseException e)
        {
            _logger.LogWarning(e, "Failed to create the movie with the title '{title}' beacuase a movie with that title already exists.", requestBody.Title);

            return BadRequest(nameof(requestBody.Title), "A movie with this title already exists.");
        }
    }

    [ScalarDefaultRequestBody("""
    {
        "NewTitle": "My updated Example Movie",
        "NewDuration": "05:43:21",
        "NewReleaseDateTimeOffsetUtc": "2222-11-22T22:11:22Z",
        "NewSynopsis": "This is an updated example for the purposes of testing!"
    }
    """)]
    [HttpPatch("{movieId}")]
    [Authorize(Policy = Policies.Permissions.Movies.Movie.PATCH)]
    public async Task<IActionResult> UpdateMovieAsync([FromRoute] Guid movieId, [FromBody] MoviePatchRequestBody requestBody)
    {
        try
        {
            await _movieService.UpdateMovieAsync(movieId, requestBody.NewTitle, requestBody.NewDuration, requestBody.NewReleaseDateTimeOffsetUtc, requestBody.NewSynopsis);

            return NoContent();
        }
        catch (MovieDoesNotExistException e)
        {
            _logger.LogWarning(e, "Failed to update the movie with the id '{movieId}' because it does not exist.", movieId);

            return NotFound();
        }
        catch (TitleAlreadyInUseException e)
        {
            _logger.LogWarning(e, "Failed to update the movie with the new title '{newTitle}' beacuase a movie with that title already exists.", requestBody.NewTitle);

            return BadRequest(nameof(requestBody.NewTitle), "A movie with this title already exists.");
        }
    }
}
