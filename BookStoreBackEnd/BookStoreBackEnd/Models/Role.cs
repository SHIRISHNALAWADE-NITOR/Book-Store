﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Role
{
    [Key]
    public int RoleId { get; set; }

    [Required]
    public string? RoleName { get; set; }

    // Navigation property
    public ICollection<User>? Users { get; set; }
}

