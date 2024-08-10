using System.ComponentModel.DataAnnotations;

public class ForgotPasswordDTO
{
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [Required]
    public DateTime DateOfBirth { get; set; }
}
