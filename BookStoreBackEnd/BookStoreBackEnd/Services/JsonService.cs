using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

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
        try
        {
            var jsonFilePath = Path.Combine(_environment.ContentRootPath, "BookData.json");
            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException("JSON file not found.", jsonFilePath);
            }

            var json = await File.ReadAllTextAsync(jsonFilePath);
            var bookDataList = JsonSerializer.Deserialize<List<BookJsonData>>(json);
            if (bookDataList == null)
            {
                throw new JsonException("Failed to deserialize JSON data.");
            }

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

            // Uncomment to log or process books
            // books.ForEach(book => Console.WriteLine(book));

            // Uncomment to add to context
            // await _context.AddRangeAsync(books);
            // await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while initializing the database.", ex);
        }
    }

    public async Task InitDatabaseAsync()
    {
        try
        {
            var jsonFilePath = Path.Combine(_environment.ContentRootPath, "BookData.json");
            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException("JSON file not found.", jsonFilePath);
            }

            List<BookJsonData> source;
            using (var reader = new StreamReader(jsonFilePath))
            {
                var json = await reader.ReadToEndAsync();
                source = JsonSerializer.Deserialize<List<BookJsonData>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (source == null)
                {
                    throw new JsonException("Failed to deserialize JSON data.");
                }
            }

            var books = source.Select(b => new Book
            {
                Isbn = int.Parse(b.Isbn10),
                Category = b.Categories,
                Title = b.Title,
                NumberOfPages = b.Num_Pages,
                //Rating = (int)Math.Round(b.Average_Rating * 100),
                Rating = b.Average_Rating,
                Author = b.Authors,
                Price = DigitsAsDecimal(long.Parse(b.Isbn10)),
                Description = b.Description,
                ImageUrl = b.Thumbnail,
                CreatedAt = new DateTime(b.Published_Year, 10, 10)
            }).ToList();

            if (!_context.Books.Any())
            {
                await _context.AddRangeAsync(books);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while initializing the database.", ex);
        }
    }

    public static decimal DigitsAsDecimal(long number)
    {
        try
        {
            long lastFourDigits = number % 1000;
            var random = new Random();
            decimal plus = random.Next(10, 100) / 100m;

            decimal result = Convert.ToDecimal(lastFourDigits);
            return result + plus;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while generating decimal from digits.", ex);
        }
    }
}
