using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

[Route("api/files")]
[ApiController]
public class FilesController : ControllerBase
{
    private readonly IFileService _fileService;
    private readonly IMapper _mapper; // Assuming AutoMapper is used for mapping

    public FilesController(IFileService fileService, IMapper mapper)
    {
        _fileService = fileService;
        _mapper = mapper;
    }

    // POST api/files/upload
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFiles([FromForm] BookFileUploadDTO bookFileUploadDto)
    {
        if (bookFileUploadDto == null)
        {
            return BadRequest("File data is missing.");
        }

        int fileId = await _fileService.UploadFilesAsync(bookFileUploadDto);

        return Ok(new { FileId = fileId });
    }
    [HttpGet("audio/{fileId}")]
    public async Task<IActionResult> GetAudioFile(int fileId)
    {
        var file = await _fileService.GetAudioFileAsync(fileId);
        if (file == null )
        {
            return NotFound();
        }

        return File(file, "audio/mp3"); // Adjust MIME type as per your file type
    }

    // GET api/files/video/{fileId}
    [HttpGet("video/{fileId}")]
    public async Task<IActionResult> GetVideoFile(int fileId)
    {
        var file = await _fileService.GetVideoFileAsync(fileId);
        if (file == null )
        {
            return NotFound();
        }

        return File(file, "video/mp4"); // Adjust MIME type as per your file type
    }

    [HttpGet("pdf/{fileId}")]
    public async Task<IActionResult> GetPdfFile(int fileId)
    {
        var pdfFile = await _fileService.GetPdfFileAsync(fileId);
        if (pdfFile == null)
        {
            return NotFound();
        }

        return File(pdfFile, "application/pdf", "filename.pdf"); // Adjust MIME type and filename as per your file type
    }
}

