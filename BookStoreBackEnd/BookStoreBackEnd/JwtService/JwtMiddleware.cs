using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreBackEnd.JwtService
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;

        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
            {
                var (userId, roleId) = ValidateAccessToken(token);
                context.Items["UserId"] = userId;
                context.Items["RoleId"] = roleId;
            }

            await _next(context);
        }

        private (string? userId, string? roleId) ValidateAccessToken(string accessToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AppSettings.JwtKey));

                tokenHandler.ValidateToken(accessToken, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    IssuerSigningKey = key,
                    ValidIssuer = AppSettings.JwtIssuer,
                    ValidAudience = AppSettings.JwtAudience,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);

                var jwtToken = validatedToken as JwtSecurityToken;

                var userId = jwtToken?.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                var roleId = jwtToken?.Claims.FirstOrDefault(x => x.Type == "roleId")?.Value;
                return (userId, roleId);
            }
            catch
            {
                return (null, null);
            }
        }
    }
    public static class JwtMiddlewareExtensions
    {
        public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtMiddleware>();
        }
    }
}
