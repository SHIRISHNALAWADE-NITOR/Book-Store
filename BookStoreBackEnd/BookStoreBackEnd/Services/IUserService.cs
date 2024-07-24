public interface IUserService
{
    Task<IEnumerable<UserDTO>> GetAllUsers();
    Task<UserDTO> GetUserById(int id);
    Task<UserDTO> AddUser(UserDTO userDto);
    Task<UserDTO> UpdateUser(UserDTO userDto);
    Task<UserDTO> GetUserByEmail(string email);
    Task DeleteUser(int id);
}
