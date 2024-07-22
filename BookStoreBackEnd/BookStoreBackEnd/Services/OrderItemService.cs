using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class OrderItemService : IOrderItemService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public OrderItemService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderItemDTO>> GetAllOrderItemsAsync()
    {
        var orderItems = await _context.OrderItems.ToListAsync();
        return _mapper.Map<IEnumerable<OrderItemDTO>>(orderItems);
    }

    public async Task<OrderItemDTO> GetOrderItemByIdAsync(int id)
    {
        var orderItem = await _context.OrderItems.FindAsync(id);
        return _mapper.Map<OrderItemDTO>(orderItem);
    }

    public async Task<OrderItemDTO> AddOrderItemAsync(OrderItemDTO orderItemDto)
    {
        var orderItem = _mapper.Map<OrderItem>(orderItemDto);
        _context.OrderItems.Add(orderItem);
        await _context.SaveChangesAsync();
        return _mapper.Map<OrderItemDTO>(orderItem);
    }

    public async Task<OrderItemDTO> UpdateOrderItemAsync(int id, OrderItemDTO orderItemDto)
    {
        var orderItem = await _context.OrderItems.FindAsync(id);
        if (orderItem == null)
        {
            return null;
        }

        _mapper.Map(orderItemDto, orderItem);
        await _context.SaveChangesAsync();
        return _mapper.Map<OrderItemDTO>(orderItem);
    }

    public async Task<bool> DeleteOrderItemAsync(int id)
    {
        var orderItem = await _context.OrderItems.FindAsync(id);
        if (orderItem == null)
        {
            return false;
        }

        _context.OrderItems.Remove(orderItem);
        await _context.SaveChangesAsync();
        return true;
    }
}
