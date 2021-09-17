using JAP_Task_1_MoviesApi.DTO;
using JAP_Task_1_MoviesApi.Services.RatingService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace JAP_Task_1_MoviesApi.Controllers
{
    [Authorize]
    public class RatingsController : BaseApiController
    {
        private readonly IRatingService _ratingService;

        public RatingsController(IRatingService ratingService)
        {

            _ratingService = ratingService;

        }

        [HttpPost("add")]
        public async Task<IActionResult> AddRating(RatingDto rating)
        {
            var response = await _ratingService.AddRating(rating.Value, rating.MovieId);

            if (!response.Success)
                return BadRequest(response);

            return Ok(response);
        }
    }
}