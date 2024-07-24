using AutoMapper;
using BookStoreBackEnd.JwtSecurity;
using Microsoft.AspNetCore.Identity;

public class UserService : IUserService
{
    private readonly IRepository<User> _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMapper _mapper;


    
    public UserService(IRepository<User> userRepository, IMapper mapper,IPasswordHasher passwordHasher)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mapper = mapper;
    }
    
    public async Task<IEnumerable<UserDTO>> GetAllUsers()
    {
        var users = await _userRepository.GetAllUsers();
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
}
