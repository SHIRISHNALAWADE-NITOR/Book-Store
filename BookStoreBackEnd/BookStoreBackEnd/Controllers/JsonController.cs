using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreBackEnd.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class JsonController : ControllerBase
    {
        private readonly IJsonService _jsonService;

        public JsonController(IJsonService jsonService)
        {
            _jsonService = jsonService;
        }

        [HttpGet("initialize")]
        public async Task<IActionResult> Initialize()
        {
            try
            {
                //await _jsonService.InitializeDatabaseAsync();
                await _jsonService.InitDatabaseAsync();
                return Ok("Database initialized successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        
    }
}
