namespace BookStoreBackEnd.Services
{
    public interface IJwtService
    {
        string GenerateJwtToken(User user);
    }
}
