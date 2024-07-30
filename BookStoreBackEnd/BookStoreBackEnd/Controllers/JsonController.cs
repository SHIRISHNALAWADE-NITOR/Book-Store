using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
                await _jsonService.InitDatabaseAsync();
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
