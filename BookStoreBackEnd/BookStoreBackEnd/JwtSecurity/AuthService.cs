
using AutoMapper;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IPasswordHasher _passwordHasher;
    //private readonly IRoleService _roleService;
    private readonly ITokenService _tokenService;
    private readonly IMapper _mapper;
    private readonly INotificationService _notificationService;
    public AuthService(IUserService userService, IPasswordHasher passwordHasher, ITokenService tokenService, IMapper mapper, INotificationService notificationService)
    {
        _userService = userService;
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        //_roleService = roleService;
        _mapper = mapper;
        _notificationService = notificationService;
    }
    public async Task<AuthResponse> Login(LoginDTO loginDto)
    {
        try
        {
            User? user = await _userService.GetUserByEmail(loginDto.Email);

            var saltedPassword = loginDto.Password + "yoSaltedPasswordyo";
            var result = _passwordHasher.VerifyPassword(user.PasswordHash, saltedPassword);

            if (!result)
            {
                throw new ApplicationException("Passwords does not match");
            }

            var token = _tokenService.CreateToken(user);
            return new AuthResponse
            {
                UserId = user.UserId,
                Token = token,
                RoleId = user.RoleId,
                Username = user.Username
            };
        }
        catch (Exception ex)
        {

            throw new ApplicationException(ex.Message);
        }
    }

    public async Task<AuthResponse> RegisterAsync(UserDTO userDto)
    {
        try
        {
            var saltedPassword = userDto.PasswordHash + "yoSaltedPasswordyo";
            // Role role = await _roleService.RoleById(userDto.RoleId); // Uncomment and use if needed
            var hashedPassword = _passwordHasher.HashPassword(saltedPassword);
            var user = new User
            {
                Name = userDto.Name,
                Username = userDto.Username,
                Email = userDto.Email,
                PhoneNumber = userDto.PhoneNumber,
                DateOfBirth = userDto.DateOfBirth,
                PasswordHash = hashedPassword,
                // Salt = salt, // Uncomment and use if needed
                RoleId = userDto.RoleId,
                // Role = role // Uncomment and use if needed
            };
            userDto.PasswordHash = hashedPassword;

            // Save user to database
            UserDTO addedUser = await _userService.AddUser(userDto);
            var token = _tokenService.CreateToken(user);

            return new AuthResponse
            {
                UserId = addedUser.UserId,
                Token = token,
                RoleId = addedUser.RoleId,
                Username = addedUser.Username
            };
        }
        catch (Exception ex)
        {
            throw new ApplicationException(" An error occurred during registration", ex);
        }
    }

    public async Task<AuthResponse> ForgotPassword(ForgotPasswordDTO forgotPasswordDto)
    {
        try
        {
            if (forgotPasswordDto == null) throw new ApplicationException("Invalid DTO");

            var user = await _userService.GetUserByEmail(forgotPasswordDto.Email);
            if (user == null)
                throw new ApplicationException("Invalid email.");

            var token = _tokenService.GeneratePasswordResetToken(user);

            return new AuthResponse
            {
                UserId = user.UserId,
                Token = token,
                RoleId = user.RoleId,
                Username = user.Username
            };
        }
        catch (Exception ex)
        {

            throw new ApplicationException(ex.Message);
        }
    }

    public async Task ResetPasswordAsync(string email, string dob, string token, string newPassword)
    {
        User user = await _userService.GetUserByEmail(email);
        if (user == null) throw new ApplicationException("User not found.");

        if (user.DateOfBirth.ToString("yyyy-MM-dd") != dob)
        {
            throw new ApplicationException("Invalid Date of Birth.");
        }

        if (!_tokenService.ValidatePasswordResetToken(user, token))
        {
            throw new ApplicationException("Invalid or expired reset token.");
        }

        var saltedPassword = newPassword + "yoSaltedPasswordyo";
        user.PasswordHash = _passwordHasher.HashPassword(saltedPassword);

        await _userService.UpdateUserAsync(user);
    }

    public async Task<AuthResponse> ForgotPasswordEmail(ForgotPasswordDTO forgotPasswordDto)
    {
        try
        {
            if (forgotPasswordDto == null) throw new ApplicationException("Invalid DTO");

            var user = await _userService.GetUserByEmail(forgotPasswordDto.Email);
            if (user == null)
                throw new ApplicationException("Invalid email.");

            var token = _tokenService.GeneratePasswordResetTokenAndOtp(user);
            return new AuthResponse
            {
                UserId = user.UserId,
                Token = token,
                RoleId = user.RoleId,
                Username = user.Username
            };
        }
        catch (Exception ex)
        {

            throw new ApplicationException(ex.Message);
        }
    }
    public async Task ResetPasswordEmailAsync(string email, string dob, string token, string newPassword, string otp)
    {
        User user = await _userService.GetUserByEmail(email);
        if (user == null) throw new ApplicationException("User not found.");

        if (user.DateOfBirth.ToString("yyyy-MM-dd") != dob)
        {
            throw new ApplicationException("Invalid Date of Birth.");
        }

        if (!_tokenService.ValidatePasswordResetTokenAndOtp(user, token, otp))
        {
            throw new ApplicationException("Invalid or expired reset token/otp.");
        }

        var saltedPassword = newPassword + "yoSaltedPasswordyo";
        user.PasswordHash = _passwordHasher.HashPassword(saltedPassword);

        await _userService.UpdateUserAsync(user);
    }
}
