using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net;
using System.Threading.Tasks;

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
    public async Task<IActionResult> GetVideoFile([FromKeyedServices("audio")] IBookFileService bookFileService, int bookId)
    {
        try
        {
            var file = await bookFileService.GetFileAsync(bookId);
            if (file == null)
            {
                return NotFound(new { Message = $"Video file with ID {bookId} not found." });
            }

            return File(file, "video/mp4"); // Adjust MIME type as per your file type
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
}
