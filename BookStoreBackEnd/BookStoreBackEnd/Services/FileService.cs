using Microsoft.EntityFrameworkCore;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class FileService : IFileService
{
    private readonly ApplicationDbContext _context;

    public FileService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> UploadFilesAsync(BookFileUploadDTO bookFileUploadDto)
    {
        try
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
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while uploading files.", ex);
        }
    }

    private async Task<byte[]> ReadFileData(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return null;
        }

        try
        {
            using (var ms = new MemoryStream())
            {
                await file.CopyToAsync(ms);
                return ms.ToArray();
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while reading file data.", ex);
        }
    }

    public async Task<byte[]> GetAudioFileAsync(int fileId)
    {
        try
        {
            var file = await _context.BookFiles
                .Where(bf => bf.Id == fileId)
                .Select(bf => bf.AudioFile)
                .FirstOrDefaultAsync();

            if (file == null)
            {
                throw new KeyNotFoundException($"Audio file with ID {fileId} not found.");
            }

            return file;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while retrieving audio file with ID {fileId}.", ex);
        }
    }

    public async Task<byte[]> GetVideoFileAsync(int fileId)
    {
        try
        {
            var file = await _context.BookFiles
                .Where(bf => bf.Id == fileId)
                .Select(bf => bf.VideoFile)
                .FirstOrDefaultAsync();

            if (file == null)
            {
                throw new KeyNotFoundException($"Video file with ID {fileId} not found.");
            }

            return file;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while retrieving video file with ID {fileId}.", ex);
        }
    }

    public async Task<byte[]> GetPdfFileAsync(int fileId)
    {
        try
        {
            var file = await _context.BookFiles
                .Where(bf => bf.Id == fileId)
                .Select(bf => bf.PdfFile)
                .FirstOrDefaultAsync();

            if (file == null)
            {
                throw new KeyNotFoundException($"PDF file with ID {fileId} not found.");
            }

            return file;
        }
        catch (Exception ex)
        {
            throw new ApplicationException($"An error occurred while retrieving PDF file with ID {fileId}.", ex);
        }
    }
}
