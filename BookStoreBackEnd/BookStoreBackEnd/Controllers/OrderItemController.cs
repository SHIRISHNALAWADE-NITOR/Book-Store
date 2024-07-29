using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class OrderItemController : ControllerBase
{
    private readonly IOrderItemService _orderItemService;

    public OrderItemController(IOrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrderItems()
    {
        try
        {
            var orderItems = await _orderItemService.GetAllOrderItemsAsync();
            return Ok(orderItems);
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
    public async Task<IActionResult> GetOrderItemById(int id)
    {
        try
        {
            var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
            if (orderItem == null)
            {
                return NotFound(new { Message = $"Order item with ID {id} not found." });
            }
            return Ok(orderItem);
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
    public async Task<IActionResult> AddOrderItem([FromBody] OrderItemDTO orderItemDto)
    {
        if (orderItemDto == null)
        {
            return BadRequest("Order item data is missing.");
        }

        try
        {
            var orderItem = await _orderItemService.AddOrderItemAsync(orderItemDto);
            return CreatedAtAction(nameof(GetOrderItemById), new { id = orderItem.OrderItemId }, orderItem);
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
    public async Task<IActionResult> UpdateOrderItem(int id, [FromBody] OrderItemDTO orderItemDto)
    {
        if (orderItemDto == null)
        {
            return BadRequest("Order item data is missing.");
        }

        try
        {
            var orderItem = await _orderItemService.UpdateOrderItemAsync(id, orderItemDto);
            if (orderItem == null)
            {
                return NotFound(new { Message = $"Order item with ID {id} not found." });
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

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderItem(int id)
    {
        try
        {
            var result = await _orderItemService.DeleteOrderItemAsync(id);
            if (!result)
            {
                return NotFound(new { Message = $"Order item with ID {id} not found." });
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
