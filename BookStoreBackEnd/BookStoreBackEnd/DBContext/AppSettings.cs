public static class AppSettings
{
    public static string DefaultConnection { get; private set; }
    public static string JwtKey { get; private set; }
    public static string JwtIssuer { get; private set; }
    public static string JwtAudience { get; private set; }

    public static void Initialize(IConfiguration configuration)
    {
        DefaultConnection = configuration.GetConnectionString("DefaultConnection");
        JwtKey = configuration["Jwt:Key"];
        JwtIssuer = configuration["Jwt:Issuer"];
        JwtAudience = configuration["Jwt:Audience"];
    }
}

