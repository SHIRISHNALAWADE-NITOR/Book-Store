public class ResetPasswordDTO
{
    public string Email { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string Token { get; set; }
    public string? Otp { get; set; }
    public string NewPassword { get; set; }
}
