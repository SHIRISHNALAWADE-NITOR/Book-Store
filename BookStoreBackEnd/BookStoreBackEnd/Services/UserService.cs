using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserService(IMapper mapper, ApplicationDbContext context)
    {
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsers()
    {
        try
        {
            var users = await _context.Users.Include(u => u.Role).ToListAsync();
            return _mapper.Map<IEnumerable<UserDTO>>(users);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving all users.", ex);
        }
    }

    public async Task<UserDTO> GetUserById(int id)
    {
        try
        {
            var user = await _context.Users.Include(u => u.Role).FirstOrDefaultAsync(u => u.UserId == id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            return _mapper.Map<UserDTO>(user);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the user by ID.", ex);
        }
    }

    public async Task<UserDTO> AddUser(UserDTO userDto)
    {
        try
        {
            var user = _mapper.Map<User>(userDto);
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while adding the user.", ex);
        }
    }

    public async Task<UserDTO> UpdateUser(UserDTO userDto)
    {
        try
        {
            var user = _mapper.Map<User>(userDto);
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return _mapper.Map<UserDTO>(user);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the user.", ex);
        }
    }

    public async Task<bool> DeleteUser(int id)
    {
        try
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with ID {id} not found.");
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the user.", ex);
        }
    }

    public async Task<User> GetUserByEmail(string email)
    {
        try
        {
            var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x => x.Email == email);
            if (user == null)
            {
                throw new KeyNotFoundException($"User with email {email} not found.");
            }
            return user;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the user by email.", ex);
        }
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        try
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the user.", ex);
        }
    }
}
