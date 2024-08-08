public interface IOrderService
{
    Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
    Task<OrderDTO> GetOrderByIdAsync(int id);
    Task<OrderDTO> AddOrderAsync(OrderDTO orderDTO);
    Task<OrderDTO> UpdateOrderAsync(int id, OrderDTO orderDTO);
    Task<bool> DeleteOrderAsync(int id);
    Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(int id);
}

