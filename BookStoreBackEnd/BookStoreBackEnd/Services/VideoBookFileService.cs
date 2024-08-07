using Microsoft.EntityFrameworkCore;

public class VideoBookFileService : IBookFileService
{
    private readonly ApplicationDbContext _context;
    public VideoBookFileService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<byte[]> GetFileAsync(int bookId)
    {
        try
        {
            var file = await _context.BookFiles
                .Where(bf => bf.BookId == bookId)
                .Select(bf => bf.VideoFile)
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
