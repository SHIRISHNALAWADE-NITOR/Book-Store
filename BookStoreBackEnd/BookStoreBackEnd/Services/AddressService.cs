using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AddressService : IAddressService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public AddressService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<AddressDTO>> GetAllAddressesAsync()
    {
        try
        {
            var addresses = await _context.Addresses.ToListAsync();
            return _mapper.Map<IEnumerable<AddressDTO>>(addresses);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving all addresses.", ex);
        }
    }

    public async Task<IEnumerable<AddressDTO>> GetAddressesByUserIdAsync(int id)
    {
        try
        {
            var address = await _context.Addresses
                                        .Where(x => x.UserId == id)
                                        .ToListAsync();
            if (address == null)
            {
                throw new KeyNotFoundException($"Address with User ID {id} not found.");
            }

            return _mapper.Map<IEnumerable<AddressDTO>>(address);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while retrieving the address service with user ID {id}.", ex);
        }
    }

    public async Task<AddressDTO> AddAddressAsync(AddressDTO addressDto)
    {
        try
        {
            var address = _mapper.Map<Address>(addressDto);
            _context.Addresses.Add(address);
            await _context.SaveChangesAsync();
            return _mapper.Map<AddressDTO>(address);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while adding a new address.", ex);
        }
    }

    public async Task<AddressDTO> UpdateAddressAsync(int id, AddressDTO addressDto)
    {
        try
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                throw new KeyNotFoundException($"Address with ID {id} not found.");
            }

            _mapper.Map(addressDto, address);
            await _context.SaveChangesAsync();
            return _mapper.Map<AddressDTO>(address);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while updating the address with ID {id}.", ex);
        }
    }

    public async Task<bool> DeleteAddressAsync(int id)
    {
        try
        {
            var address = await _context.Addresses.FindAsync(id);
            if (address == null)
            {
                return false;
            }

            _context.Addresses.Remove(address);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while deleting the address with ID {id}.", ex);
        }
    }
}
