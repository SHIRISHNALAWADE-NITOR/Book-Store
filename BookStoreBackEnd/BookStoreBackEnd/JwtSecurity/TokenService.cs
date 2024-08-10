using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;
    private readonly INotificationService _notificationService;
    private readonly Random _random = new();

    public TokenService(IConfiguration configuration, INotificationService notificationService)
    {
        _configuration = configuration;
        _notificationService = notificationService;
    }

    public string CreateToken(User user)
    {
        // Determine role based on RoleId
        var role = user.RoleId == 1 ? "admin" : "user";

        // Create claims
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Name, user.Username),
            new Claim(ClaimTypes.Role, role) // Ensure role is added to claims
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public string GeneratePasswordResetToken(User user)
    {
        var validMinutes = 10;
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("DOB", user.DateOfBirth.ToString("yyyy-MM-dd")),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(validMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    public string GeneratePasswordResetTokenAndOtp(User user)
    {
        var otp = _random.Next(100000, 999999).ToString();
        var validMinutes = 10;
        var claims = new[]
        {
            new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim("DOB", user.DateOfBirth.ToString("yyyy-MM-dd")),
            new Claim ("OTP",otp)
        };

        MailData data = new MailData
        {
            EmailToId = user.Email,
            EmailToName = user.Name,
            EmailSubject = "Your OTP Code for Password Reset",
            EmailBody = $@"
Dear {user.Name},

We received a request to reset your password. Here is your one-time password (OTP):

OTP Code: {otp}

Please enter this code in the application to proceed with resetting your password. This OTP is valid for {validMinutes} minutes and can only be used once.

If you did not request a password reset, please ignore this message or contact support if you have any concerns.

Best regards,

The Pustak Paradise Team
"

        };
        //Console.WriteLine(data.EmailBody);
        _notificationService.SendMail(data);
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(validMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public bool ValidatePasswordResetTokenAndOtp(User user, string token, string otp)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var email = jwtToken.Claims.First(x => x.Type == ClaimTypes.Email).Value;
            var dobClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "DOB")?.Value;
            var otpClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "OTP")?.Value;

            if (dobClaim == null || user.UserId.ToString() != userId || user.Email != email || user.DateOfBirth.ToString("yyyy-MM-dd") != dobClaim || otpClaim != otp)
            {
                return false;
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
    public bool ValidatePasswordResetToken(User user, string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _configuration["Jwt:Issuer"],
                ValidAudience = _configuration["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var email = jwtToken.Claims.First(x => x.Type == ClaimTypes.Email).Value;
            var dobClaim = jwtToken.Claims.FirstOrDefault(x => x.Type == "DOB")?.Value;

            if (dobClaim == null || user.UserId.ToString() != userId || user.Email != email || user.DateOfBirth.ToString("yyyy-MM-dd") != dobClaim)
            {
                return false;
            }

            return true;
        }
        catch
        {
            return false;
        }
    }
}
