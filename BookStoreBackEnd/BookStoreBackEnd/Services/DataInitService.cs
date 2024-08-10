using System.Text.Json;

public class DataInitService : IDataInitService
{
    private readonly ApplicationDbContext _context;
    private readonly IWebHostEnvironment _environment;
    private readonly IAuthService _authService;

    public DataInitService(ApplicationDbContext context, IWebHostEnvironment environment, IAuthService authService)
    {
        _context = context;
        _environment = environment;
        _authService = authService;
    }

    public async Task InitDatabasePreviousAsync()
    {
        try
        {
            var jsonFilePath = Path.Combine(_environment.ContentRootPath, "BookDataPrevious.json");
            if (!File.Exists(jsonFilePath))
            {
                throw new FileNotFoundException("JSON file not found.", jsonFilePath);
            }

            List<Book> source;
            using (var reader = new StreamReader(jsonFilePath))
            {
                var json = await reader.ReadToEndAsync();
                source = JsonSerializer.Deserialize<List<Book>>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                if (source == null)
                {
                    throw new JsonException("Failed to deserialize JSON data.");
                }
            }

            var books = source.Select(b => new Book
            {
                Isbn = b.Isbn,
                Category = b.Category,
                Title = b.Title,
                NumberOfPages = b.NumberOfPages,
                //Rating = (int)Math.Round(b.Average_Rating * 100),
                Rating = b.Rating,
                Author = b.Author,
                Price = b.Price,
                Description = b.Description,
                ImageUrl = b.ImageUrl,
                CreatedAt = b.CreatedAt,
                Quantity = b.Isbn % 100,
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
    public async Task Initialize()
    {
        try
        {
            if (!_context.Roles.Any())
            {
                // Define roles
                var roles = new Role[]
                {
                    new Role { RoleName = "admin" },
                    new Role { RoleName = "user" }
                };

                _context.Roles.AddRange(roles);
                await _context.SaveChangesAsync();
            }

            if (!_context.Users.Any())
            {
                // Define users
                var users = new UserDTO[]
                {
                    new UserDTO
                    {
                        Name = "Admin User",
                        DateOfBirth = new DateTime(1980, 1, 1),
                        PhoneNumber = "123-456-7890",
                        Username = "admin",
                        Email = "admin@gmail.com",
                        RoleId = 1,
                        PasswordHash = "Aa@#$2211"
                    },
                    new UserDTO
                    {
                        Name = "Regular User",
                        DateOfBirth = new DateTime(1990, 6, 15),
                        PhoneNumber = "098-765-4321",
                        Username = "user",
                        Email = "user@gmail.com",
                        RoleId = 2,
                        PasswordHash = "Uu@#$2211"
                    }
                };

                foreach (var user in users)
                {
                    await _authService.RegisterAsync(user);
                }
            }

            if (!_context.Addresses.Any())
            {
                // Define addresses
                var addresses = new Address[]
                {
                    new Address
                    {
                        UserId = 1,
                        Street = "Plot No 702, Tower 1, Gardenia",
                        City = "Pune",
                        State = "Maharashtra",
                        PostalCode = "411045",
                        Country = "India"
                    },
                    new Address
                    {
                        UserId = 2,
                        Street = "A-202, Sai Plaza",
                        City = "Mumbai",
                        State = "Maharashtra",
                        PostalCode = "400064",
                        Country = "India"
                    },
                    new Address
                    {
                        UserId = 2,
                        Street = "3rd Floor, Crystal Tower",
                        City = "Nashik",
                        State = "Maharashtra",
                        PostalCode = "422001",
                        Country = "India"
                    },
                    new Address
                    {
                        UserId = 2,
                        Street = "Plot No 58, Near Ram Mandir",
                        City = "Nagpur",
                        State = "Maharashtra",
                        PostalCode = "440001",
                        Country = "India"
                    }
                };

                _context.Addresses.AddRange(addresses);
                await _context.SaveChangesAsync();
            }

            if (!_context.CartItems.Any())
            {
                // Define cart items
                var cartItems = new CartItem[]
                {
                    new CartItem
                    {
                        UserId = 2,
                        BookId = 1,
                        Quantity = 1
                    },
                    new CartItem
                    {
                        UserId = 2,
                        BookId = 2,
                        Quantity = 2
                    },
                    new CartItem
                    {
                        UserId = 2,
                        BookId = 3,
                        Quantity = 1
                    },
                    new CartItem
                    {
                        UserId = 2,
                        BookId = 10,
                        Quantity = 5
                    }
                };

                _context.CartItems.AddRange(cartItems);
                await _context.SaveChangesAsync();
            }

            if (!_context.Orders.Any())
            {
                // Define orders
                var orders = new Order[]
                {
                    new Order
                    {
                        UserId = 2,
                        OrderDate = DateTime.Now.AddDays(-5),
                        TotalAmount = 299.99M,
                        ShippingAddressId = 4
                    },
                    new Order
                    {
                        UserId = 2,
                        OrderDate = DateTime.Now.AddDays(-2),
                        TotalAmount = 649.97M,
                        ShippingAddressId = 2
                    }
                };

                _context.Orders.AddRange(orders);
                await _context.SaveChangesAsync();
            }

            if (!_context.OrderItems.Any())
            {
                // Define order items
                var orderItems = new OrderItem[]
                {
                    new OrderItem
                    {
                        OrderId = 1,
                        BookId = 1,
                        Quantity = 1,
                        Price = 299.99M
                    },
                    new OrderItem
                    {
                        OrderId = 2,
                        BookId = 2,
                        Quantity = 2,
                        Price = 199.99M
                    },
                    new OrderItem
                    {
                        OrderId = 2,
                        BookId = 3,
                        Quantity = 1,
                        Price = 249.99M
                    }
                };

                _context.OrderItems.AddRange(orderItems);
                await _context.SaveChangesAsync();
            }

            if (!_context.BookFiles.Any())
            {
                // Define book files
                var bookFiles = new BookFile[]
                {
                    new BookFile
                    {
                        BookId = 1,
                        AudioFile = LoadFileData("Resources/audio.mp3"),
                        VideoFile = LoadFileData("Resources/video.mp4"),
                        PdfFile = LoadFileData("Resources/book.pdf")
                    },
                    new BookFile
                    {
                        BookId = 2,
                        AudioFile = LoadFileData("Resources/audio.mp3"),
                        VideoFile = LoadFileData("Resources/video.mp4"),
                        PdfFile = LoadFileData("Resources/book.pdf")
                    },
                    new BookFile
                    {
                        BookId = 3,
                        AudioFile = LoadFileData("Resources/audio.mp3"),
                        VideoFile = LoadFileData("Resources/video.mp4"),
                        PdfFile = LoadFileData("Resources/book.pdf")
                    }
                };

                _context.BookFiles.AddRange(bookFiles);
                await _context.SaveChangesAsync();
            }
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"File not found: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    private static byte[] LoadFileData(string filePath)
    {
        try
        {
            if (File.Exists(filePath))
            {
                return File.ReadAllBytes(filePath);
            }
            else
            {
                throw new FileNotFoundException($"File not found: {filePath}");
            }
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Error reading file: {ex.Message}");
            throw; // Rethrow the exception to be handled by the outer catch block
        }
    }
}

