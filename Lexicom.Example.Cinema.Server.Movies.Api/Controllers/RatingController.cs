using Lexicom.AspNetCore.Controllers.Amenities;
using Lexicom.Example.Cinema.Server.Movies.Api.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.Example.Cinema.Server.Movies.Api.Controllers;
[Authorize]
[ApiController]
[Route("api/movies/rating")]
public class RatingController : LexicomController
{
    [HttpPost]
    public async Task<IActionResult> CreateRatingAsync(RatingPostRequestBody requestBody)
    {
        try
        {

            return NoContent();
        }
        catch
        {
            return null;
        }
    }
}
