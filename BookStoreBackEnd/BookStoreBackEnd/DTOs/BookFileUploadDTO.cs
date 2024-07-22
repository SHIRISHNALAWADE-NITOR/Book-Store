public class BookFileUploadDTO
{
    public int BookId { get; set; }
    public IFormFile AudioFile { get; set; }
    public IFormFile VideoFile { get; set; }
    public IFormFile PdfFile { get; set; }
}
