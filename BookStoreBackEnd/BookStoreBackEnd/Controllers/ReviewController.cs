using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReviews()
    {
        try
        {
            var reviews = await _reviewService.GetAllReviewsAsync();
            return Ok(reviews);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Internal server error: {ex.Message}" });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetReviewById(int id)
    {
        try
        {
            var review = await _reviewService.GetReviewByIdAsync(id);
            if (review == null)
            {
                return NotFound(new { Message = $"Review with ID {id} not found." });
            }
            return Ok(review);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Internal server error: {ex.Message}" });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddReview([FromBody] ReviewDTO reviewDto)
    {
        if (reviewDto == null)
        {
            return BadRequest("Review data is missing.");
        }

        try
        {
            var review = await _reviewService.AddReviewAsync(reviewDto);
            return CreatedAtAction(nameof(GetReviewById), new { id = review.ReviewId }, review);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Internal server error: {ex.Message}" });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateReview(int id, [FromBody] ReviewDTO reviewDto)
    {
        if (reviewDto == null)
        {
            return BadRequest("Review data is missing.");
        }

        try
        {
            var review = await _reviewService.UpdateReviewAsync(id, reviewDto);
            if (review == null)
            {
                return NotFound(new { Message = $"Review with ID {id} not found." });
            }
            return NoContent();
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Internal server error: {ex.Message}" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteReview(int id)
    {
        try
        {
            var result = await _reviewService.DeleteReviewAsync(id);
            if (!result)
            {
                return NotFound(new { Message = $"Review with ID {id} not found." });
            }
            return NoContent();
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = $"Internal server error: {ex.Message}" });
        }
    }
}
