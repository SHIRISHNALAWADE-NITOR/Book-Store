using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public OrderService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
    {
        var orders = await _context.Orders.Include(o => o.OrderItems).ToListAsync();
        return _mapper.Map<IEnumerable<OrderDTO>>(orders);
    }

    public async Task<OrderDTO> GetOrderByIdAsync(int id)
    {
        var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderId == id);
        return _mapper.Map<OrderDTO>(order);
    }

    public async Task<OrderDTO> AddOrderAsync(OrderDTO orderDto)
    {
        var order = _mapper.Map<Order>(orderDto);
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();
        return _mapper.Map<OrderDTO>(order);
    }

    public async Task<OrderDTO> UpdateOrderAsync(int id, OrderDTO orderDto)
    {
        var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderId == id);
        if (order == null)
        {
            return null;
        }

        _mapper.Map(orderDto, order);
        await _context.SaveChangesAsync();
        return _mapper.Map<OrderDTO>(order);
    }

    public async Task<bool> DeleteOrderAsync(int id)
    {
        var order = await _context.Orders.FindAsync(id);
        if (order == null)
        {
            return false;
        }

        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
        return true;
    }
}
