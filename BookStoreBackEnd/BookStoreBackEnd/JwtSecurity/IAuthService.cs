public interface IAuthService
{
    //Task<AuthResponse> Register(UserDTO userDto);
    Task<AuthResponse> RegisterAsync(UserDTO userDto);
    Task<AuthResponse> Login(LoginDTO loginDto);
}
