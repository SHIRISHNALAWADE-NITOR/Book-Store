using AutoMapper;
using Microsoft.EntityFrameworkCore;

public class ReviewService : IReviewService
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public ReviewService(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ReviewDTO>> GetAllReviewsAsync()
    {
        var reviews = await _context.Reviews.ToListAsync();
        return _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
    }

    public async Task<ReviewDTO> GetReviewByIdAsync(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        return _mapper.Map<ReviewDTO>(review);
    }

    public async Task<ReviewDTO> AddReviewAsync(ReviewDTO reviewDto)
    {
        var review = _mapper.Map<Review>(reviewDto);
        _context.Reviews.Add(review);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReviewDTO>(review);
    }

    public async Task<ReviewDTO> UpdateReviewAsync(int id, ReviewDTO reviewDto)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
        {
            return null;
        }

        _mapper.Map(reviewDto, review);
        await _context.SaveChangesAsync();
        return _mapper.Map<ReviewDTO>(review);
    }

    public async Task<bool> DeleteReviewAsync(int id)
    {
        var review = await _context.Reviews.FindAsync(id);
        if (review == null)
        {
            return false;
        }

        _context.Reviews.Remove(review);
        await _context.SaveChangesAsync();
        return true;
    }
}

