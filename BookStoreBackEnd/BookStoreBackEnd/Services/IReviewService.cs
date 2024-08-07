public interface IReviewService
{
    Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync();
    Task<ReviewDTO> GetReviewByIdAsync(int id);
    Task<ReviewDTO> AddReviewAsync(ReviewDTO reviewDto);
    Task<ReviewDTO> UpdateReviewAsync(int id, ReviewDTO reviewDto);
    Task<bool> DeleteReviewAsync(int id);
}

