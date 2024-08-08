using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class AddressController : ControllerBase
{
    private readonly IAddressService _addressService;

    public AddressController(IAddressService addressService)
    {
        _addressService = addressService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllAddresses()
    {
        try
        {
            var addresses = await _addressService.GetAllAddressesAsync();
            return Ok(addresses);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAddressByUserId(int id)
    {
        try
        {
            var address = await _addressService.GetAddressesByUserIdAsync(id);
            if (address == null)
            {
                return NotFound(new { Message = $"Address with USer ID {id} not found." });
            }
            return Ok(address);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddAddress([FromBody] AddressDTO addressDto)
    {
        try
        {
            var address = await _addressService.AddAddressAsync(addressDto);
            return Ok(address);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressDTO addressDto)
    {
        try
        {
            var address = await _addressService.UpdateAddressAsync(id, addressDto);
            if (address == null)
            {
                return NotFound(new { Message = $"Address with ID {id} not found." });
            }
            return Ok(address);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        try
        {
            var result = await _addressService.DeleteAddressAsync(id);
            if (!result)
            {
                return NotFound(new { Message = $"Address with ID {id} not found." });
            }
            return NoContent();
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }
}