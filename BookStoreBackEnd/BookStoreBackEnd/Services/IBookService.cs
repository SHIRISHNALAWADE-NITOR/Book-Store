public interface IBookService
{
    Task<IEnumerable<BookDTO>> GetAllBooksAsync();
    Task<BookDTO> GetBookByIdAsync(int id);
    Task<BookDTO> AddBookAsync(BookDTO bookDto);
    Task<BookDTO> UpdateBookAsync(int id, BookDTO bookDto);
    Task<bool> DeleteBookAsync(int id);
}
