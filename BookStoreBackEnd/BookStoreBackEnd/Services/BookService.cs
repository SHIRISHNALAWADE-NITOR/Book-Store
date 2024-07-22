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
        var books = await _context.Books.ToListAsync();
        return _mapper.Map<IEnumerable<BookDTO>>(books);
    }

    public async Task<BookDTO> GetBookByIdAsync(int id)
    {
        var book = await _context.Books.FindAsync(id);
        return _mapper.Map<BookDTO>(book);
    }

    public async Task<BookDTO> AddBookAsync(BookDTO bookDto)
    {
        var book = _mapper.Map<Book>(bookDto);
        _context.Books.Add(book);
        await _context.SaveChangesAsync();
        return _mapper.Map<BookDTO>(book);
    }

    public async Task<BookDTO> UpdateBookAsync(int id, BookDTO bookDto)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return null;
        }

        _mapper.Map(bookDto, book);
        await _context.SaveChangesAsync();
        return _mapper.Map<BookDTO>(book);
    }

    public async Task<bool> DeleteBookAsync(int id)
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
}
