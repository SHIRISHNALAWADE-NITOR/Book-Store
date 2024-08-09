using AutoMapper;
using Microsoft.AspNetCore.Mvc;

[Route("api/files")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly IMapper _mapper;

    public FilesController(IFileService fileService, IMapper mapper)
    {
        _fileService = fileService;
        _mapper = mapper;
    }

    [HttpPost("upload")]
    public async Task<IActionResult> UploadFiles([FromForm] BookFileUploadDTO bookFileUploadDto)
    {
        if (bookFileUploadDto == null)
        {
            return BadRequest("File data is missing.");
        }

        try
        {
            int fileId = await _fileService.UploadFilesAsync(bookFileUploadDto);
            return Ok(new { FileId = fileId });
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

    [HttpGet("audio/{bookId}")]
    public async Task<IActionResult> GetAudioFile([FromKeyedServices("audio")] IBookFileService bookFileService, int bookId)
    {
        try
        {
            var file = await bookFileService.GetFileAsync(bookId);
            if (file == null)
            {
                return NotFound(new { Message = $"Audio file with ID {bookId} not found." });
            }

            return File(file, "audio/mp3"); // Adjust MIME type as per your file type
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

    [HttpGet("video/{bookId}")]
    public async Task<IActionResult> GetVideoFile([FromKeyedServices("video")] IBookFileService bookFileService, int bookId)
    {
        try
        {
            var file = await bookFileService.GetFileAsync(bookId);
            if (file == null)
            {
                return NotFound(new { Message = $"Video file with ID {bookId} not found." });
            }

            //return File(file, "video/mp4"); 
            return File(file, "video/mp4");
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }


    [HttpGet("pdf/{bookId}")]
    public async Task<IActionResult> GetPdfFile([FromKeyedServices("pdf")] IBookFileService bookFileService, int bookId)
    {
        try
        {
            var pdfFile = await _fileService.GetPdfFileAsync(bookId);
            if (pdfFile == null)
            {
                return NotFound(new { Message = $"PDF file with ID {bookId} not found." });
            }

            return File(pdfFile, "application/pdf", "filename.pdf"); // Adjust MIME type and filename as per your file type
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }


    [HttpDelete("{fileId}")]
    public async Task<IActionResult> DeleteFileById(int fileId)
    {
        try
        {
            bool result = await _fileService.DeleteFileAsync(fileId);
            if (result)
            {
                return NoContent(); // Success with no content
            }
            else
            {
                return NotFound(new { Message = $"File with ID {fileId} not found." });
            }
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }
    [HttpGet("list")]
    public async Task<IActionResult> GetListOfAllFiles()
    {
        try
        {
            var files = await _fileService.GetAllFilesAsync();
            if (files == null || !files.Any())
            {
                return NotFound(new { Message = "No files found." });
            }

            return Ok(files);
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

}