using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class Book
{
    [Key]
    public int BookId { get; set; }
    public int Isbn { get; set; }
    public string Category { get; set; }
    public int NumberOfPages { get; set; }
    public decimal Rating { get; set; }
    public int Quantity { get; set; }

    [Required]
    public string? Title { get; set; }

    [Required]
    public string? Author { get; set; }

    [Required]
    public decimal Price { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    // Navigation property
    public ICollection<Review>? Reviews { get; set; }
}

