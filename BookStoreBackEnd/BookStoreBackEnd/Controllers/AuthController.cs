using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using BookStoreBackEnd.JwtSecurity;

namespace BookStoreBackEnd.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService;
            _logger = logger;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticationModel model)
        {
            _logger.LogInformation($"Attempting to authenticate user: {model.Username}");

            var response = _authService.Authenticate(model);

            if (response == null)
            {
                _logger.LogWarning($"Failed to authenticate user: {model.Username}");
                return BadRequest(new { message = "Username or password is incorrect" });
            }

            _logger.LogInformation($"User authenticated successfully: {model.Username}");

            return Ok(new { username = response.Username, token = response.Token });
        }
    }
}
