public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetAllUsers();
    Task<UserDTO> GetUserById(int id);
    Task<UserDTO> AddUser(UserDTO userDto);
    Task<UserDTO> UpdateUser(UserDTO userDto);
    Task<User> UpdateUserAsync(User user);
    Task<User> GetUserByEmail(string email);
    Task<bool> DeleteUser(int id);
}
