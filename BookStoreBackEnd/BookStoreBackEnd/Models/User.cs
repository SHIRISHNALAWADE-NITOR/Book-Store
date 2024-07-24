using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;

public class User
{
    [Key]
    public int UserId { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNumber { get; set; }

    [Required]
    public string? Username { get; set; }

    [Required]
    public string? PasswordHash { get; set; }

    [Required]
    [EmailAddress]
    public string? Email { get; set; }

    [Required]
    public int RoleId { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    public ICollection<Address>? Addresses { get; set; }

    [ForeignKey("RoleId")]
    public Role? Role { get; set; }

    public ICollection<Order> Orders { get; set; }
}
