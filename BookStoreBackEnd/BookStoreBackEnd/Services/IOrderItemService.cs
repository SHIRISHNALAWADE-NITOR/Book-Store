public interface IOrderItemService
{
    Task<IEnumerable<OrderItemDTO>> GetAllOrderItemsAsync();
    Task<OrderItemDTO> GetOrderItemByIdAsync(int id);
    Task<OrderItemDTO> AddOrderItemAsync(OrderItemDTO orderItemDto);
    Task<OrderItemDTO> UpdateOrderItemAsync(int id, OrderItemDTO orderItemDto);
    Task<bool> DeleteOrderItemAsync(int id);
}

