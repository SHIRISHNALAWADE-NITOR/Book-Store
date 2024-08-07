public class BookDTO
{
    public int BookId { get; set; }
    public int Isbn { get; set; }
    public string Category { get; set; }
    public int NumberOfPages { get; set; }
    public decimal Rating { get; set;}
    public string Title { get; set; }
    public string Author { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
}
