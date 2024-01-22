using BusinessLayer.DTOs.Review;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReviewController : ControllerBase
    {
        private readonly IReviewService _reviewService;

        public ReviewController(IReviewService reviewService)
        {
            _reviewService = reviewService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReviews(string? format)
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            if (reviews == null || reviews.Count == 0)
            {
                return NotFound("No reviews were found.");
            }
            return Ok(reviews);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetReview(int id, string? format)
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound($"No review with ID {id} was found.");
            }
            return Ok(review);
        }

        [HttpPost]
        public async Task<IActionResult> CreateReview(ReviewCreateUpdateDTO review)
        {
            try
            {
                var result = await _reviewService.CreateReviewAsync(review);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateReview(int id, ReviewCreateUpdateDTO review)
        {
            try
            {
                var result = await _reviewService.UpdateReviewAsync(id, review);
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteReview(int id)
        {
            var result = await _reviewService.DeleteReviewAsync(id);
            if (result)
            {
                return Ok();
            }
            return NotFound($"No review with ID {id} was found.");
        }
    }
}
