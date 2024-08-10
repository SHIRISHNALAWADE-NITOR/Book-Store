using Microsoft.AspNetCore.Mvc;

namespace BookStoreBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DataController : ControllerBase
    {
        private readonly IDataInitService _dataService;

        public DataController(IDataInitService dataService)
        {
            _dataService = dataService;
        }

        [HttpGet("initialize")]
        public async Task<IActionResult> Initialize()
        {
            try
            {
                await _dataService.InitDatabasePreviousAsync();
                await _dataService.Initialize();
                return Ok("Database initialized successfully.");
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
}
