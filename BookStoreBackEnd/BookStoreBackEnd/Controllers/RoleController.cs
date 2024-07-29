using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BookStoreBackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRoles()
        {
            try
            {
                var roles = await _roleService.GetAllRoles();
                return Ok(roles);
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
        public async Task<IActionResult> GetRoleById(int id)
        {
            try
            {
                var role = await _roleService.GetRoleById(id);
                if (role == null)
                {
                    return NotFound(new { Message = $"Role with ID {id} not found." });
                }
                return Ok(role);
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
        public async Task<IActionResult> AddRole([FromBody] RoleDTO roleDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var role = await _roleService.AddRole(roleDto);
                return CreatedAtAction(nameof(GetRoleById), new { id = role.RoleId }, role);
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
        public async Task<IActionResult> UpdateRole(int id, [FromBody] RoleDTO roleDto)
        {
            if (id != roleDto.RoleId || !ModelState.IsValid)
            {
                return BadRequest(new { Message = "Invalid role ID or model state is invalid." });
            }

            try
            {
                var role = await _roleService.UpdateRole(roleDto);
                if (role == null)
                {
                    return NotFound(new { Message = $"Role with ID {id} not found." });
                }
                return Ok(role);
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
        public async Task<IActionResult> DeleteRole(int id)
        {
            try
            {
                var result = await _roleService.DeleteRole(id);

                if (!result)
                {
                    return NotFound(new { Message = $"Role with ID {id} not found." });
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
}
