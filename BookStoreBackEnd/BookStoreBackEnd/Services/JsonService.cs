
using Microsoft.AspNetCore.Http;
using System;
using System.Globalization;
using System.Text.Json;

public class JsonService : IJsonService
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;
    public JsonService(ApplicationDbContext context, IWebHostEnvironment environment)
    {
        _context = context;
        _environment = environment;
    }
    public async Task InitializeDatabaseAsync()
    {
        var jsonFilePath = Path.Combine(_environment.ContentRootPath, "BookData.json");
        var json = File.ReadAllText(jsonFilePath);
        var bookDataList = JsonSerializer.Deserialize<List<BookJsonData>>(json);
        //Console.WriteLine(bookDataList);
        Console.WriteLine("----------------------------------");
        //bookDataList.ForEach(b => Console.WriteLine(b));

        var books = bookDataList.Select(b => new Book
        {
            Isbn = int.Parse(b.Isbn10),
            Category = b.Categories,
            NumberOfPages = b.Num_Pages,
            Rating = (int)Math.Round(b.Average_Rating * 100),
            Author = b.Authors,
            Price = DigitsAsDecimal(long.Parse(b.Isbn10)),
            Description = b.Description,
            ImageUrl = b.Thumbnail,
            CreatedAt = new DateTime(b.Published_Year, 10, 10)
        }).ToList();
        books.ForEach(book =>
        {
            Console.WriteLine(book);
        });
        //await _context.AddRangeAsync(books);
        //await _context.SaveChangesAsync();
    }
    public async Task InitDatabaseAsync()
    {
        List<BookJsonData> source = new List<BookJsonData>();
        using (StreamReader r = new StreamReader("BookData.json"))
        {
            string json = r.ReadToEnd();
            //Console.WriteLine(json);
            source = JsonSerializer.Deserialize<List<BookJsonData>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});
        }
        //var books = JsonSerializer.Deserialize<List<Book>>(json, new JsonSerializerOptions
        //{
        //    PropertyNameCaseInsensitive = true // Ignore case when matching properties
        //});
        //source.ForEach(b=> Console.WriteLine(b));
        List<Book> books = source.Select(b => new Book
        {
            Isbn = int.Parse(b.Isbn10),
            Category = b.Categories,
            Title = b.Title,
            NumberOfPages = b.Num_Pages,
            Rating = (int)Math.Round(b.Average_Rating* 100),
            Author = b.Authors,
            Price = DigitsAsDecimal(long.Parse(b.Isbn10)),
            Description = b.Description,
            ImageUrl = b.Thumbnail,
            CreatedAt = new DateTime(b.Published_Year,10,10)
        }).ToList();
        //books.ForEach(book => {Console.WriteLine(book);});
        //source.ForEach(b => Console.WriteLine(b));
        if (!_context.Books.Any())
        {
            await _context.AddRangeAsync(books);
            await _context.SaveChangesAsync();
        }
    }
    public static decimal DigitsAsDecimal(long number)
    {
        long lastFourDigits = number % 1000;
        Random random = new Random();
        decimal plus = random.Next(10, 99 + 1)/100;

        decimal result = Convert.ToDecimal(lastFourDigits);

        return result+plus;
    }
}

