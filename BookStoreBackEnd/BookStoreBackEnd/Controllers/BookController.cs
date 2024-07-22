using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class BookController : ControllerBase
{
    private readonly IBookService _bookService;

    public BookController(IBookService bookService)
    {
        _bookService = bookService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAllBooks()
    {
        var books = await _bookService.GetAllBooksAsync();
        return Ok(books);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        var book = await _bookService.GetBookByIdAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        return Ok(book);
    }

    [HttpPost]
    public async Task<IActionResult> AddBook([FromBody] BookDTO bookDto)
    {
        var book = await _bookService.AddBookAsync(bookDto);
        return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, book);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO bookDto)
    {
        var book = await _bookService.UpdateBookAsync(id, bookDto);
        if (book == null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        var result = await _bookService.DeleteBookAsync(id);
        if (!result)
        {
            return NotFound();
        }
        return NoContent();
    }
}

