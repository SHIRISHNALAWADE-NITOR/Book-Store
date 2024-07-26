public class BookDTO
{
    public int BookId { get; set; }
    public int Isbn { get; set; }
    public string Category { get; set; }
    public int NumberOfPages { get; set; }
    public int Rating { get; set; } // Use `int` for integer rating, change if needed
    public string Title { get; set; } // Required
    public string Author { get; set; } // Required
    public decimal Price { get; set; } // Required
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; } // Required
}
