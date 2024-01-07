using BusinessLayer.DTOs.Review;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IReviewService : IBaseService
    {
        Task<List<ReviewBasicDTO>> GetAllReviewsAsync();
        Task<ReviewBasicDTO?> GetReviewByIdAsync(int id);
        Task<ReviewCreateUpdateDTO> CreateReviewAsync(ReviewCreateUpdateDTO newReview);
        Task<ReviewCreateUpdateDTO> UpdateReviewAsync(int id, ReviewCreateUpdateDTO updatedReview);
        Task<bool> DeleteReviewAsync(int id);
    }
}
