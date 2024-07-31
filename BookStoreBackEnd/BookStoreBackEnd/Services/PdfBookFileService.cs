using Microsoft.EntityFrameworkCore;

public class PdfBookFileService : IBookFileService
{
    private readonly ApplicationDbContext _context;
    public PdfBookFileService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<byte[]> GetFileAsync(int bookId)
    {
        try
        {
            var file = await _context.BookFiles
                .Where(bf => bf.BookId == bookId)
                .Select(bf => bf.PdfFile)
                .FirstOrDefaultAsync();

            if (file == null)
            {
                throw new KeyNotFoundException($"Audio file with book ID {bookId} not found.");
            }

            return file;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while retrieving audio file with book ID {bookId} .", ex);
        }
    }
}
