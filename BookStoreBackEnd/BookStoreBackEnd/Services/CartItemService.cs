using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        try
        {
            var cartItems = await _context.CartItems.ToListAsync();
            return _mapper.Map<IEnumerable<CartItemDTO>>(cartItems);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving cart items.", ex);
        }
    }

    public async Task<CartItemDTO> GetCartItemByIdAsync(int id)
    {
        try
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                throw new KeyNotFoundException($"Cart item with ID {id} not found.");
            }

            return _mapper.Map<CartItemDTO>(cartItem);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while retrieving cart item with ID {id}.", ex);
        }
    }

    public async Task<CartItemDTO> AddCartItemAsync(CartItemDTO cartItemDto)
    {
        try
        {
            var cartItem = _mapper.Map<CartItem>(cartItemDto);
            _context.CartItems.Add(cartItem);
            await _context.SaveChangesAsync();
            return _mapper.Map<CartItemDTO>(cartItem);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while adding the cart item.", ex);
        }
    }

    public async Task<CartItemDTO> UpdateCartItemAsync(int id, CartItemDTO cartItemDto)
    {
        try
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                throw new KeyNotFoundException($"Cart item with ID {id} not found.");
            }

            _mapper.Map(cartItemDto, cartItem);
            await _context.SaveChangesAsync();
            return _mapper.Map<CartItemDTO>(cartItem);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while updating cart item with ID {id}.", ex);
        }
    }

    public async Task<bool> DeleteCartItemAsync(int id)
    {
        try
        {
            var cartItem = await _context.CartItems.FindAsync(id);
            if (cartItem == null)
            {
                throw new KeyNotFoundException($"Cart item with ID {id} not found.");
            }

            _context.CartItems.Remove(cartItem);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while deleting cart item with ID {id}.", ex);
        }
    }

    public async Task<bool> DeleteCartAsync(int userId)
    {
        try
        {
            var cartItems = await _context.CartItems
                                          .Where(x => x.UserId == userId)
                                          .ToListAsync();

            if (cartItems == null)
            {
                throw new KeyNotFoundException($"Cart items with User ID {userId} not found.");
            }

            _context.CartItems.RemoveRange(cartItems);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while deleting cart with User ID {userId}.", ex);
        }
    }

    public async Task<IEnumerable<CartItemDTO>> GetCartItemsByUserIdAsync(int userId)
    {
        try
        {
            var cartItem = await _context.CartItems
                                         .Where (x => x.UserId == userId)
                                         .ToListAsync();
            if (cartItem == null)
            {
                throw new KeyNotFoundException($"Cart item with User ID {userId} not found.");
            }

            return _mapper.Map<IEnumerable<CartItemDTO>>(cartItem);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while retrieving cart item with user ID {userId}.", ex);
        }
    }
}
