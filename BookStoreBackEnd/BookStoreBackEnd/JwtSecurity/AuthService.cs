
using Azure.Core;
using Microsoft.AspNetCore.Identity;
using System;

public class AuthService : IAuthService
{
    private readonly IUserService _userService;
    private readonly IPasswordHasher _passwordHasher;
    //private readonly IRoleService _roleService;
    private readonly ITokenService _tokenService;
    public AuthService (IUserService userService, IPasswordHasher passwordHasher, ITokenService tokenService)
    {
        _userService = userService; 
        _passwordHasher = passwordHasher;
        _tokenService = tokenService;
        //_roleService = roleService;
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
                PasswordHash = hashedPassword, // Null is okay because user is not yet created
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
}
