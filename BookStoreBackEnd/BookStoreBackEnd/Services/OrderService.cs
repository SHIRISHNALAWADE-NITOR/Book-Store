using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Data;

public class OrderService : IOrderService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;

    public OrderService(ApplicationDbContext context, IMapper mapper, INotificationService notificationService)
    {
        _context = context;
        _mapper = mapper;
        _notificationService = notificationService;
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
            var user = _context.Users.FirstOrDefault(u => u.UserId == order.UserId);
            if (user == null) throw new KeyNotFoundException($"user with user ID {order.UserId} Not Found");
            var orderItems = string.Join("\n", order.OrderItems.Select(item => $"- {item.OrderItemId}==> BookId-{item.BookId}  Quantity-{item.Quantity} Price-{item.Price:C}"));
            var emailBody = $@"
Dear Customer,

Thank you for your order! We are pleased to confirm that we have received your order with the following details:

Order ID: {order.OrderId}
Order Date: {order.OrderDate:MMMM d, yyyy}
Total Amount: {order.TotalAmount:C}

Order Items:
{orderItems}

Best regards,
The Pustak Paradise Team
";
            MailData data = new MailData
            {
                EmailToId = user.Email,
                EmailToName = user.Name,
                EmailSubject = $"Order #{order.OrderId} Confirmed: Thank You for Shopping with Us!",
                EmailBody = emailBody
            };
            _notificationService.SendMail(data);
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
