using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BookStoreBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }
        [AllowAnonymous]
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody] UserDTO userDto)
        {
            //AuthResponse authResponse = await _authService.RegisterAsync(userDto);
            //return Ok(authResponse);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                AuthResponse authResponse = await _authService.RegisterAsync(userDto);
                return Ok(authResponse);
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
        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //AuthResponse authResponse = await _authService.Login(request);
            //return Ok(authResponse);
            try
            {
                AuthResponse authResponse = await _authService.Login(request);
                return Ok(authResponse);
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
