using Lexicom.AspNetCore.Controllers.Amenities;
using Lexicom.Example.Cinema.Server.Movies.Api.Contracts;
using Lexicom.Example.Cinema.Server.Movies.Application.Services;
using Lexicom.Example.Cinema.Server.Shared.Authentication;
using Lexicom.Scalar;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lexicom.Example.Cinema.Server.Movies.Api.Controllers;
[Authorize]
[ApiController]
[Route("api/movies")]
public class ReviewController : LexicomController
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet("{movieId}/review/summary")]
    [AllowAnonymous]
    public async Task<IActionResult> GetReviewSummaryAsync([FromRoute] Guid movieId)
    {
        return NoContent();
        //try
        //{
        //    return Ok(new ReviewGetSummaryResponseBody
        //    {
        //        AverageRating = ?,
        //        RatingsDistribution = ?,
        //    });
        //}
        //catch
        //{

        //}
    }

    [HttpGet("{movieId}/review")]
    [AllowAnonymous]
    public async Task<IActionResult> GetReviewsAsync([FromRoute] Guid movieId)
    {
        return NoContent();
        //try
        //{
        //    return Ok(new ReviewGetResponseBody
        //    {
        //        TotalReviews = ?,
        //        Reviews = ?,
        //    });
        //}
        //catch
        //{

        //}
    }

    [ScalarDefaultRequestBody("""
    {
        "Rating": 5,
        "Text": "01:23:45"
    }
    """)]
    [HttpPost("{movieId}/review")]
    [Authorize(Policy = Policies.Permissions.Movies.Review.POST)]
    public async Task<IActionResult> CreateReviewAsync([FromRoute] Guid movieId, [FromBody] ReviewPostRequestBody requestBody)
    {
        return NoContent();
        //try
        //{

        //    return Ok(new ReviewPostResponseBody
        //    {
        //        CreatedReviewId = ?,
        //    });
        //}
        //catch
        //{
        //    return null;
        //}
    }

    [ScalarDefaultRequestBody("""
    {
        "NewRating": 5,
        "NewText": "01:23:45"
    }
    """)]
    [HttpPatch("/review/{reviewId}")]
    [Authorize(Policy = Policies.Permissions.Movies.Review.POST)]
    public async Task<IActionResult> UpdateReviewAsync([FromRoute] Guid reviewId, [FromBody] ReviewPostRequestBody requestBody)
    {
        return NoContent();
        //try
        //{

        //    return NoContent();
        //}
        //catch
        //{
        //    return null;
        //}
    }
}
