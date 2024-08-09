public interface IAuthService
{
    //Task<AuthResponse> Register(UserDTO userDto);
    Task<AuthResponse> RegisterAsync(UserDTO userDto);
    Task<AuthResponse> Login(LoginDTO loginDto);
    Task<AuthResponse> ForgotPassword(ForgotPasswordDTO forgotPasswordDto);
    Task ResetPasswordAsync(string email, string dob, string token, string newPassword);
}
