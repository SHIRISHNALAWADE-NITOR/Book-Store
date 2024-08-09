using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Review
{
    [Key]
    public int ReviewId { get; set; }

    [Required]
    public int BookId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    [Range(1, 5)]
    public decimal Rating { get; set; }

    public string? Comment { get; set; }

    [Required]
    public DateTime CreatedAt { get; set; }

    // Navigation properties
    [ForeignKey("BookId")]
    public Book? Book { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }
}
