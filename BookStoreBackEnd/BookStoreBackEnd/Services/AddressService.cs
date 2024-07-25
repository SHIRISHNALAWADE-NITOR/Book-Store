using AutoMapper;
using Microsoft.EntityFrameworkCore;

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
        var addresses = await _context.Addresses.ToListAsync();
        return _mapper.Map<IEnumerable<AddressDTO>>(addresses);
    }

    public async Task<AddressDTO> GetAddressByIdAsync(int id)
    {
        var address = await _context.Addresses.FindAsync(id);
        return _mapper.Map<AddressDTO>(address);
    }

    public async Task<AddressDTO> AddAddressAsync(AddressDTO addressDto)
    {
        var address = _mapper.Map<Address>(addressDto);
        _context.Addresses.Add(address);
        await _context.SaveChangesAsync();
        return _mapper.Map<AddressDTO>(address);
    }

    public async Task<AddressDTO> UpdateAddressAsync(int id, AddressDTO addressDto)
    {
        var address = await _context.Addresses.FindAsync(id);
        if (address == null)
        {
            return null;
        }

        _mapper.Map(addressDto, address);
        await _context.SaveChangesAsync();
        return _mapper.Map<AddressDTO>(address);
    }

    public async Task<bool> DeleteAddressAsync(int id)
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
}

