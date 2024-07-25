using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
        var orderItems = await _orderItemService.GetAllOrderItemsAsync();
        return Ok(orderItems);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderItemById(int id)
    {
        var orderItem = await _orderItemService.GetOrderItemByIdAsync(id);
        if (orderItem == null)
        {
            return NotFound();
        }
        return Ok(orderItem);
    }

    [HttpPost]
    public async Task<IActionResult> AddOrderItem([FromBody] OrderItemDTO orderItemDto)
    {
        var orderItem = await _orderItemService.AddOrderItemAsync(orderItemDto);
        return CreatedAtAction(nameof(GetOrderItemById), new { id = orderItem.OrderItemId }, orderItem);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderItem(int id, [FromBody] OrderItemDTO orderItemDto)
    {
        var orderItem = await _orderItemService.UpdateOrderItemAsync(id, orderItemDto);
        if (orderItem == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderItem(int id)
    {
        var result = await _orderItemService.DeleteOrderItemAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}

