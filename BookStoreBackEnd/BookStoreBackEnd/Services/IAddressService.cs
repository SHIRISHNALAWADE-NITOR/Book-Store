public interface IAddressService
{
    Task<IEnumerable<AddressDTO>> GetAllAddressesAsync();
    Task<IEnumerable<AddressDTO>> GetAddressesByUserIdAsync(int id);
    Task<AddressDTO> AddAddressAsync(AddressDTO addressDto);
    Task<AddressDTO> UpdateAddressAsync(int id, AddressDTO addressDto);
    Task<bool> DeleteAddressAsync(int id);
}

