using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCrypt.Net;

namespace BookStoreBackEnd.JwtSecurity
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IPasswordHasher _passwordHasher;

        private readonly string _secretKey = "sdhbfjckhkjhijankjnjner324brjhwbcjh"; // Replace with your secret key
        private readonly string _issuer = "user"; // Replace with your issuer
        private readonly string _audience = "admin"; // Replace with your audience
        private readonly IRepository<User> _userRepository;
        public AuthService(IPasswordHasher passwordHasher, ILogger<AuthService> logger,IRepository<User> userRepository)
        {
            _passwordHasher = passwordHasher;
            _logger = logger;
            _userRepository = userRepository;
        }

        public AuthenticationResponse Authenticate(AuthenticationModel authenticationModel)
        {
            try
            {
                var user = _userRepository.GetByUsername(authenticationModel.Username);
                Console.WriteLine(user.RoleId);

                if (user != null && user.PasswordHash == authenticationModel.Password)
                {
                    // Authentication successful, generate JWT token
                    var token = GenerateJwtToken(user.Username);
                    return new AuthenticationResponse { Username = user.Username, Token = token ,RoleId=user.RoleId};
                }
                else
                {
                    // Authentication failed
                    return null;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception occurred while authenticating user: {ex.Message}");
                return null; // Handle exception as needed
            }
        }


        private string GenerateJwtToken(string username)
        {
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, username),
                new Claim(ClaimTypes.Name, username),
                // Add other claims as needed (e.g., roles)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1), // Set token expiration
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
