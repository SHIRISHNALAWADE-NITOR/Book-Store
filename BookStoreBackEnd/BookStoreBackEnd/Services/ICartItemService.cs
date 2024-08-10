public interface ICartItemService
{
    Task<IEnumerable<CartItemDTO>> GetAllCartItemsAsync();
    Task<CartItemDTO> GetCartItemByIdAsync(int id);
    Task<CartItemDTO> AddCartItemAsync(CartItemDTO cartItemDto);
    Task<CartItemDTO> UpdateCartItemAsync(int id, CartItemDTO cartItemDto);
    Task<bool> DeleteCartItemAsync(int id);
    Task<bool> DeleteCartAsync(int userId);
    Task<IEnumerable<CartItemDTO>> GetCartItemsByUserIdAsync(int userId);
}

