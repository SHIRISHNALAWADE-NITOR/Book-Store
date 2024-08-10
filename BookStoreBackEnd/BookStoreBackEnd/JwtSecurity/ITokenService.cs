public interface ITokenService
{
    string CreateToken(User user);
    string GeneratePasswordResetToken(User user);
    string GeneratePasswordResetTokenAndOtp(User user);
    bool ValidatePasswordResetToken(User user, string token);
    bool ValidatePasswordResetTokenAndOtp(User user, string token, string otp);
}
