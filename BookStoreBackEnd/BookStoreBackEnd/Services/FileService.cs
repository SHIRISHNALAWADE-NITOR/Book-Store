using Microsoft.EntityFrameworkCore;

public class FileService : IFileService
{
    private readonly ApplicationDbContext _context;

    public FileService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> UploadFilesAsync(BookFileUploadDTO bookFileUploadDto)
    {
        var bookFile = new BookFile
        {
            BookId = bookFileUploadDto.BookId,
            AudioFile = await ReadFileData(bookFileUploadDto.AudioFile),
            VideoFile = await ReadFileData(bookFileUploadDto.VideoFile),
            PdfFile = await ReadFileData(bookFileUploadDto.PdfFile)
        };

        _context.BookFiles.Add(bookFile);
        await _context.SaveChangesAsync();

        return bookFile.Id;
    }

    private async Task<byte[]> ReadFileData(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return null;
        }

        using (var ms = new MemoryStream())
        {
            await file.CopyToAsync(ms);
            return ms.ToArray();
        }
    }
    public async Task<byte[]> GetAudioFileAsync(int fileId)
    {
        var file = await _context.BookFiles
            .Where(bf => bf.Id == fileId)
            .Select(bf => bf.AudioFile)
            .FirstOrDefaultAsync();

        return file;
    }

    public async Task<byte[]> GetVideoFileAsync(int fileId)
    {
        var file = await _context.BookFiles
            .Where(bf => bf.Id == fileId)
            .Select(bf => bf.VideoFile)
            .FirstOrDefaultAsync();

        return file;
    }
    public async Task<byte[]> GetPdfFileAsync(int fileId)
    {
        var file = await _context.BookFiles
            .Where(bf => bf.Id == fileId)
            .Select(bf => bf.PdfFile)
            .FirstOrDefaultAsync();

        return file;
    }
}
