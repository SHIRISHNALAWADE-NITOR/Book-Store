using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        try
        {
            var orders = await _context.Orders.Include(o => o.OrderItems).ToListAsync();
            return _mapper.Map<IEnumerable<OrderDTO>>(orders);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving all orders.", ex);
        }
    }

    public async Task<OrderDTO> GetOrderByIdAsync(int id)
    {
        try
        {
            var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }
            return _mapper.Map<OrderDTO>(order);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the order by ID.", ex);
        }
    }

    public async Task<IEnumerable<OrderDTO>> GetOrdersByUserIdAsync(int id)
    {
        try
        {
            var order = await _context.Orders.Include(o => o.OrderItems).Where(o => o.UserId == id).ToListAsync();
            if (order == null)
            {
                throw new KeyNotFoundException($"Order for User ID {id} not found.");
            }
            return _mapper.Map<IEnumerable<OrderDTO>>(order);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the order by User ID.", ex);
        }
    }

    public async Task<OrderDTO> AddOrderAsync(OrderDTO orderDto)
    {
        try
        {
            var order = _mapper.Map<Order>(orderDto);
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDTO>(order);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while adding the order.", ex);
        }
    }

    public async Task<OrderDTO> UpdateOrderAsync(int id, OrderDTO orderDto)
    {
        try
        {
            var order = await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderId == id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Order with ID {id} not found.");
            }

            _mapper.Map(orderDto, order);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderDTO>(order);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the order.", ex);
        }
    }

    public async Task<bool> DeleteOrderAsync(int id)
    {
        try
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
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the order.", ex);
        }
    }
}
