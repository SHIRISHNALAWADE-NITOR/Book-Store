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
        try
        {
            var orderItems = await _context.OrderItems.ToListAsync();
            return _mapper.Map<IEnumerable<OrderItemDTO>>(orderItems);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving all order items.", ex);
        }
    }

    public async Task<OrderItemDTO> GetOrderItemByIdAsync(int id)
    {
        try
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                throw new KeyNotFoundException($"OrderItem with ID {id} not found.");
            }
            return _mapper.Map<OrderItemDTO>(orderItem);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the order item by ID.", ex);
        }
    }

    public async Task<OrderItemDTO> AddOrderItemAsync(OrderItemDTO orderItemDto)
    {
        try
        {
            var orderItem = _mapper.Map<OrderItem>(orderItemDto);
            var book = await _context.Books.FirstOrDefaultAsync(b => b.BookId == orderItem.BookId);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {orderItem.BookId} not found.");
            }
            if (orderItem.Quantity > book.Quantity)
            {
                throw new ApplicationException("The Quantity of books ordered is more than Quantity available");
            }
            _context.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderItemDTO>(orderItem);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while adding the order item.", ex);
        }
    }

    public async Task<OrderItemDTO> UpdateOrderItemAsync(int id, OrderItemDTO orderItemDto)
    {
        try
        {
            var orderItem = await _context.OrderItems.FindAsync(id);
            if (orderItem == null)
            {
                throw new KeyNotFoundException($"OrderItem with ID {id} not found.");
            }

            _mapper.Map(orderItemDto, orderItem);
            await _context.SaveChangesAsync();
            return _mapper.Map<OrderItemDTO>(orderItem);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the order item.", ex);
        }
    }

    public async Task<bool> DeleteOrderItemAsync(int id)
    {
        try
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
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the order item.", ex);
        }
    }
}
