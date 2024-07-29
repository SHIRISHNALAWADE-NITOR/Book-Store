using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

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
        try
        {
            var reviews = await _context.Reviews.ToListAsync();
            return _mapper.Map<IEnumerable<ReviewDTO>>(reviews);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving all reviews.", ex);
        }
    }

    public async Task<ReviewDTO> GetReviewByIdAsync(int id)
    {
        try
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                throw new KeyNotFoundException($"Review with ID {id} not found.");
            }
            return _mapper.Map<ReviewDTO>(review);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while retrieving the review by ID.", ex);
        }
    }

    public async Task<ReviewDTO> AddReviewAsync(ReviewDTO reviewDto)
    {
        try
        {
            var review = _mapper.Map<Review>(reviewDto);
            _context.Reviews.Add(review);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReviewDTO>(review);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while adding the review.", ex);
        }
    }

    public async Task<ReviewDTO> UpdateReviewAsync(int id, ReviewDTO reviewDto)
    {
        try
        {
            var review = await _context.Reviews.FindAsync(id);
            if (review == null)
            {
                throw new KeyNotFoundException($"Review with ID {id} not found.");
            }

            _mapper.Map(reviewDto, review);
            await _context.SaveChangesAsync();
            return _mapper.Map<ReviewDTO>(review);
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while updating the review.", ex);
        }
    }

    public async Task<bool> DeleteReviewAsync(int id)
    {
        try
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
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while deleting the review.", ex);
        }
    }
}
