using BusinessLayer.DTOs.Review;
using BusinessLayer.Mapper;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class ReviewService : BaseService, IReviewService
    {
        private readonly BookHubDBContext _dbContext;

        public ReviewService(BookHubDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<ReviewCreateUpdateDTO> CreateReviewAsync(ReviewCreateUpdateDTO reviewCreateDTO)
        {
            var review = EntityMapper.MapToReview(reviewCreateDTO);
            if (review == null || review.Rating < 0 || review.Rating > 5)
            {
                throw new Exception("There was an error creating Review");
            }
            _dbContext.Reviews.Add(review);
            await SaveAsync(true);
            return reviewCreateDTO;
        }

        public async Task<bool> DeleteReviewAsync(int id)
        {
            var review = await _dbContext.Reviews.FindAsync(id);
            if (review == null)
            {
                return false;
            }
            _dbContext.Reviews.Remove(review);
            await SaveAsync(true);
            return true;
        }

        public async Task<ReviewDTO?> GetReviewByIdAsync(int id)
        {
            var review = await _dbContext.Reviews.Include(c => c.Customer).Include(b => b.Book).Where(r => r.Id == id).FirstOrDefaultAsync();
            if (review == null)
            {
                return null;
            }
            return DTOMapper.MapToReviewDTO(review);

        }

        public async Task<List<ReviewDTO>> GetAllReviewsAsync()
        {
            var reviews = await _dbContext.Reviews.Include(c => c.Customer).Include(b => b.Book).ToListAsync();
            return reviews.Select(r => DTOMapper.MapToReviewDTO(r)).ToList();

        }

        public async Task<ReviewCreateUpdateDTO> UpdateReviewAsync(int id, ReviewCreateUpdateDTO reviewUpdateDTO)
        {
            var review = await _dbContext.Reviews.FindAsync(id);
            if (review == null || reviewUpdateDTO.Rating < 0 || reviewUpdateDTO.Rating > 5)
            {
                throw new Exception("There was an error updating Review");
            }
            review.BookId = reviewUpdateDTO.BookId;
            review.CustomerId = reviewUpdateDTO.CustomerId;
            review.Rating = reviewUpdateDTO.Rating;
            review.Comment = reviewUpdateDTO.Comment;
            await SaveAsync(true);
            return reviewUpdateDTO;

        }
    }
}
