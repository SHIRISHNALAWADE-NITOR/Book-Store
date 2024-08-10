public interface IBookFileService
{
    Task<byte[]> GetFileAsync(int bookId);
}
