using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

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
        try
        {
            var books = await _bookService.GetAllBooksAsync();
            return Ok(books);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBookById(int id)
    {
        try
        {
            var book = await _bookService.GetBookByIdAsync(id);
            if (book == null)
            {
                return NotFound(new { Message = $"Book with ID {id} not found." });
            }
            return Ok(book);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

    [HttpPost]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<IActionResult> AddBook([FromBody] BookDTO bookDto)
    {
        try
        {
            var book = await _bookService.AddBookAsync(bookDto);
            return CreatedAtAction(nameof(GetBookById), new { id = book.BookId }, book);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    [Authorize(Policy = "RequireAdminRole")]
    public async Task<IActionResult> UpdateBook(int id, [FromBody] BookDTO bookDto)
    {
        try
        {
            var book = await _bookService.UpdateBookAsync(id, bookDto);
            if (book == null)
            {
                return NotFound(new { Message = $"Book with ID {id} not found." });
            }
            return Ok(book);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }
    [Authorize(Policy = "RequireAdminRole")]
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteBook(int id)
    {
        try
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
            {
                return NotFound(new { Message = $"Book with ID {id} not found." });
            }
            return Ok();
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }
    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetBookByCategory(string category)
    {
        try
        {
            var book = await _bookService.GetBooksByCategoryAsync(category);
            if (book == null)
            {
                return NotFound(new { Message = $"Book with Category {category} not found." });
            }
            return Ok(book);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }
    [HttpGet("category")]
    public async Task<IActionResult> GetCategory()
    {
        try
        {
            var book = await _bookService.GetCategoriesAsync();
            if (book == null)
            {
                return NotFound(new { Message = $"Error while getting categories" });
            }
            return Ok(book);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

    [HttpGet("category/topbooks")]
    public async Task<IActionResult> GetTopBookOfCategory()
    {
        try
        {
            var book = await _bookService.GetTopBooksOfEachCategoryAsync(1);
            if (book == null)
            {
                return NotFound(new { Message = $"Error getting Top Book of Category." });
            }
            return Ok(book);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }
    [HttpGet("year/{year}")]
    public async Task<IActionResult> GetBookByYear(int year)
    {
        try
        {
            var book = await _bookService.GetBooksByYear(year);
            if (book == null)
            {
                return NotFound(new { Message = $"Book from {year} not found." });
            }
            return Ok(book);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }
}
