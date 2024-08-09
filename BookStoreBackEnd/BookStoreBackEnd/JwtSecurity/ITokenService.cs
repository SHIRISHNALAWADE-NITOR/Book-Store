public interface ITokenService
{
    string CreateToken(User user);
    string GeneratePasswordResetToken(User user);
    bool ValidatePasswordResetToken(User user, string token);
}
