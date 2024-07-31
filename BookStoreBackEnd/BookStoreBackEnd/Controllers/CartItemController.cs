using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class CartItemController : ControllerBase
{
    private readonly ICartItemService _cartItemService;

    public CartItemController(ICartItemService cartItemService)
    {
        _cartItemService = cartItemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllCartItems()
    {
        try
        {
            var cartItems = await _cartItemService.GetAllCartItemsAsync();
            return Ok(cartItems);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCartItemById(int id)
    {
        try
        {
            var cartItem = await _cartItemService.GetCartItemByIdAsync(id);
            if (cartItem == null)
            {
                return NotFound(new { Message = $"CartItem with ID {id} not found." });
            }
            return Ok(cartItem);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddCartItem([FromBody] CartItemDTO cartItemDto)
    {
        try
        {
            var cartItem = await _cartItemService.AddCartItemAsync(cartItemDto);
            return CreatedAtAction(nameof(GetCartItemById), new { id = cartItem.CartItemId }, cartItem);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCartItem(int id, [FromBody] CartItemDTO cartItemDto)
    {
        try
        {
            var cartItem = await _cartItemService.UpdateCartItemAsync(id, cartItemDto);
            if (cartItem == null)
            {
                return NotFound(new { Message = $"CartItem with ID {id} not found." });
            }
            return NoContent();
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCartItem(int id)
    {
        try
        {
            var result = await _cartItemService.DeleteCartItemAsync(id);
            if (!result)
            {
                return NotFound(new { Message = $"CartItem with ID {id} not found." });
            }
            return NoContent();
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }
    [HttpDelete("user/{userId}")]
    public async Task<IActionResult> DeleteCart(int userId)
    {
        try
        {
            var result = await _cartItemService.DeleteCartAsync(userId);
            if (!result)
            {
                return NotFound(new { Message = $"CartItem with user ID {userId} not found." });
            }
            return NoContent();
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetCartItemByUserId(int userId)
    {
        try
        {
            var cartItem = await _cartItemService.GetCartItemsByUserIdAsync(userId);
            if (cartItem == null)
            {
                return NotFound(new { Message = $"CartItem with user ID {userId} not found." });
            }
            return Ok(cartItem);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }
}
