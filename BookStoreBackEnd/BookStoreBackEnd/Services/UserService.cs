using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public UserService(IRepository<User> userRepository, IMapper mapper, ApplicationDbContext context)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _context = context;
    }

    public async Task<IEnumerable<UserDTO>> GetAllUsers()
    {
        var users = await _userRepository.GetAll();
        return _mapper.Map<IEnumerable<UserDTO>>(users);
    }

    public async Task<UserDTO> GetUserById(int id)
    {
        var user = await _userRepository.GetById(id);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO> AddUser(UserDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);
        user = await _userRepository.Add(user);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task<UserDTO> UpdateUser(UserDTO userDto)
    {
        var user = _mapper.Map<User>(userDto);
        user = await _userRepository.Update(user);
        return _mapper.Map<UserDTO>(user);
    }

    public async Task DeleteUser(int id)
    {
        await _userRepository.Delete(id);
    }

    public async Task<UserDTO> GetUserByEmail(string email)
    {
        var user = await _context.Users.Include(x => x.Role).FirstOrDefaultAsync(x=>x.Email == email);  
        return _mapper.Map<UserDTO>(user);
    }
}
