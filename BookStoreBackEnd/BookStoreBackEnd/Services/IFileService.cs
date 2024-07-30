public interface IFileService
{
    Task<int> UploadFilesAsync(BookFileUploadDTO bookFileUploadDto);
    Task<byte[]> GetAudioFileAsync(int fileId);
    Task<byte[]> GetVideoFileAsync(int fileId);
    Task<byte[]> GetPdfFileAsync(int fileId);
}
