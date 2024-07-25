using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class CartItemService : ICartItemService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public CartItemService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CartItemDTO>> GetAllCartItemsAsync()
    {
        var cartItems = await _context.CartItems.ToListAsync();
        return _mapper.Map<IEnumerable<CartItemDTO>>(cartItems);
    }

    public async Task<CartItemDTO> GetCartItemByIdAsync(int id)
    {
        var cartItem = await _context.CartItems.FindAsync(id);
        return _mapper.Map<CartItemDTO>(cartItem);
    }

    public async Task<CartItemDTO> AddCartItemAsync(CartItemDTO cartItemDto)
    {
        var cartItem = _mapper.Map<CartItem>(cartItemDto);
        _context.CartItems.Add(cartItem);
        await _context.SaveChangesAsync();
        return _mapper.Map<CartItemDTO>(cartItem);
    }

    public async Task<CartItemDTO> UpdateCartItemAsync(int id, CartItemDTO cartItemDto)
    {
        var cartItem = await _context.CartItems.FindAsync(id);
        if (cartItem == null)
        {
            return null;
        }

        _mapper.Map(cartItemDto, cartItem);
        await _context.SaveChangesAsync();
        return _mapper.Map<CartItemDTO>(cartItem);
    }

    public async Task<bool> DeleteCartItemAsync(int id)
    {
        var cartItem = await _context.CartItems.FindAsync(id);
        if (cartItem == null)
        {
            return false;
        }

        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();
        return true;
    }
}
