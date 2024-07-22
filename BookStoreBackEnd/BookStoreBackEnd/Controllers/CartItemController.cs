using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        var cartItems = await _cartItemService.GetAllCartItemsAsync();
        return Ok(cartItems);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetCartItemById(int id)
    {
        var cartItem = await _cartItemService.GetCartItemByIdAsync(id);
        if (cartItem == null)
        {
            return NotFound();
        }
        return Ok(cartItem);
    }

    [HttpPost]
    public async Task<IActionResult> AddCartItem([FromBody] CartItemDTO cartItemDto)
    {
        var cartItem = await _cartItemService.AddCartItemAsync(cartItemDto);
        return CreatedAtAction(nameof(GetCartItemById), new { id = cartItem.CartItemId }, cartItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCartItem(int id, [FromBody] CartItemDTO cartItemDto)
    {
        var cartItem = await _cartItemService.UpdateCartItemAsync(id, cartItemDto);
        if (cartItem == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCartItem(int id)
    {
        var result = await _cartItemService.DeleteCartItemAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}

