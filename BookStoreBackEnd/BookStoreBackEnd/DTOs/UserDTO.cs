public class UserDTO
{
    public int UserId { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public int RoleId = 1;
    public string? PasswordHash { get; set; }
}
