using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        var addresses = await _addressService.GetAllAddressesAsync();
        return Ok(addresses);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetAddressById(int id)
    {
        var address = await _addressService.GetAddressByIdAsync(id);
        if (address == null)
        {
            return NotFound();
        }
        return Ok(address);
    }

    [HttpPost]
    public async Task<IActionResult> AddAddress([FromBody] AddressDTO addressDto)
    {
        var address = await _addressService.AddAddressAsync(addressDto);
        return CreatedAtAction(nameof(GetAddressById), new { id = address.AddressId }, address);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAddress(int id, [FromBody] AddressDTO addressDto)
    {
        var address = await _addressService.UpdateAddressAsync(id, addressDto);
        if (address == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAddress(int id)
    {
        var result = await _addressService.DeleteAddressAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}

