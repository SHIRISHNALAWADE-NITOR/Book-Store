public class BookJsonData
{
    public long? Isbn13 { get; set; }
    public string Isbn10 { get; set; }
    public string? Title { get; set; }
    public string? Subtitle { get; set; }
    public string? Authors { get; set; }
    public string? Categories { get; set; }
    public string? Thumbnail { get; set; }
    public string? Description { get; set; }
    public int Published_Year { get; set; }
    public decimal Average_Rating { get; set; }
    public int Num_Pages { get; set; }
    public int Ratings_Count { get; set; }

    public override string ToString()
    {
        return $"Title: {Title}\n" +
               $"Subtitle: {Subtitle}\n" +
               $"Authors: {Authors}\n" +
               $"ISBN-13: {Isbn13}\n" +
               $"ISBN-10: {Isbn10}\n" +
               $"Categories: {Categories}\n" +
               $"Published Year: {Published_Year}\n" +
               $"Average Rating: {Average_Rating}\n" +
               $"Number of Pages: {Num_Pages}\n" +
               $"Ratings Count: {Ratings_Count}\n" +
               $"Description: {Description}\n" +
               $"Thumbnail: {Thumbnail}\n";
    }
}

