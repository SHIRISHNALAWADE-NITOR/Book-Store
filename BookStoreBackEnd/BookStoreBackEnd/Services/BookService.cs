using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class BookService : IBookService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public BookService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<BookDTO>> GetAllBooksAsync()
    {
        try
        {
            var books = await _context.Books.Where(b => b.Quantity != 0).ToListAsync();
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving all books.", ex);
        }
    }

    public async Task<BookDTO> GetBookByIdAsync(int id)
    {
        try
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }

            return _mapper.Map<BookDTO>(book);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while retrieving the book with ID {id}.", ex);
        }
    }

    public async Task<BookDTO> AddBookAsync(BookDTO bookDto)
    {
        try
        {
            var book = _mapper.Map<Book>(bookDto);
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return _mapper.Map<BookDTO>(book);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while adding a new book.", ex);
        }
    }

    public async Task<BookDTO> UpdateBookAsync(int id, BookDTO bookDto)
    {
        try
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                throw new KeyNotFoundException($"Book with ID {id} not found.");
            }

            _mapper.Map(bookDto, book);
            await _context.SaveChangesAsync();
            return _mapper.Map<BookDTO>(book);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while updating the book with ID {id}.", ex);
        }
    }

    public async Task<bool> DeleteBookAsync(int id)
    {
        try
        {
            var book = await _context.Books.FindAsync(id);
            if (book == null)
            {
                return false;
            }

            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return true;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while deleting the book with ID {id}.", ex);
        }
    }

    public async Task<IEnumerable<BookDTO>> GetBooksByCategoryAsync(string category)
    {
        try
        {
            var books = await _context.Books
                                      .Where(b => b.Category == category)
                                      .ToListAsync();
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving all books.", ex);
        }
    }

    public async Task<IEnumerable<CategoryDTO>> GetCategoriesAsync()
    {
        try
        {
            var groupedCategories = _context.Books
                                           .GroupBy(b => b.Category)
                                           .Select(g => new CategoryDTO
                                           {
                                               Category = g.Key,
                                               Count = g.Count()
                                           });
            return groupedCategories;
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving all books.", ex);
        }
    }

    public async Task<IEnumerable<BookDTO>> GetTopBooksOfEachCategoryAsync(int no)
    {
        try
        {
            var books = await _context.Books
                                      .GroupBy(b => b.Category)
                                      .Select(g => g.OrderByDescending(b => b.Rating).FirstOrDefault())
                                      .ToListAsync();
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving all books.", ex);
        }
    }

    public async Task<IEnumerable<BookDTO>> GetBooksByYear(int year)
    {
        try
        {
            var books = await _context.Books
                                      .Where(b => b.CreatedAt.Year == year)
                                      .ToListAsync();
            return _mapper.Map<IEnumerable<BookDTO>>(books);
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while retrieving {year} books.", ex);
        }
    }
}
