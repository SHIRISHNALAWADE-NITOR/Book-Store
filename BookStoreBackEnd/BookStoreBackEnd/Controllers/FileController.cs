using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

    [HttpGet("audio/{fileId}")]
    public async Task<IActionResult> GetAudioFile(int fileId)
    {
        try
        {
            var file = await _fileService.GetAudioFileAsync(fileId);
            if (file == null)
            {
                return NotFound(new { Message = $"Audio file with ID {fileId} not found." });
            }

            return File(file, "audio/mp3"); // Adjust MIME type as per your file type
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

    [HttpGet("video/{fileId}")]
    public async Task<IActionResult> GetVideoFile(int fileId)
    {
        try
        {
            var file = await _fileService.GetVideoFileAsync(fileId);
            if (file == null)
            {
                return NotFound(new { Message = $"Video file with ID {fileId} not found." });
            }

            return File(file, "video/mp4"); // Adjust MIME type as per your file type
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }

    [HttpGet("pdf/{fileId}")]
    public async Task<IActionResult> GetPdfFile(int fileId)
    {
        try
        {
            var pdfFile = await _fileService.GetPdfFileAsync(fileId);
            if (pdfFile == null)
            {
                return NotFound(new { Message = $"PDF file with ID {fileId} not found." });
            }

            return File(pdfFile, "application/pdf", "filename.pdf"); // Adjust MIME type and filename as per your file type
        }
        catch (ApplicationException ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, new { Message = ex.Message });
        }
    }
}
