using BusinessLayer.DTOs;
using DataAccessLayer.Models;
using System.Linq;

namespace BusinessLayer.Mapper
{
    public static class DTOMapper
    {
        public static AuthorDTO MapToAuthorDTO(this Author author)
        {
            return new AuthorDTO
            {
                Name = author.Name
            };
        }

        public static BookDTO MapToBookDTO(this Book book)
        {
            return new BookDTO
            {
                Title = book.Title,
                Authors = book.Authors.Select(ab => new AuthorDTO
                {
                    Name = ab.Name
                }).ToList(),
                Genres = book.Genres.Select(gb => new GenreDTO
                {
                    Name = gb.Name
                }).ToList(),
                PublisherName = book.Publisher?.Name,
                Price = book.Price,
                Description = book.Description
            };
        }

        public static CustomerDTO MapToCustomerDTO(this Customer customer)
        {
            return new CustomerDTO
            {
                Username = customer.Username
            };
        }

        public static GenreDTO MapToGenreDTO(this Genre genre)
        {
            return new GenreDTO
            {
                Name = genre.Name
            };
        }

        public static PublisherDTO MapToPublisherDTO(this Publisher publisher)
        {
            return new PublisherDTO
            {
                Name = publisher.Name
            };
        }

        public static PurchaseHistoryDTO MapToPurchaseHistoryDTO(this PurchaseHistory purchaseHistory)
        {
            return new PurchaseHistoryDTO
            {
                BookTitle = purchaseHistory.Book?.Title,
                CustomerUsername = purchaseHistory.Customer?.Username,
                PurchaseDate = purchaseHistory.PurchaseDate
            };
        }

        public static ReviewDTO MapToReviewDTO(this Review review)
        {
            return new ReviewDTO
            {
                BookTitle = review.Book?.Title,
                CustomerUsername = review.Customer?.Username,
                Rating = review.Rating,
                Comment = review.Comment
            };
        }

        public static WishlistDTO MapToWishlistDTO(this Wishlist wishlist)
        {
            return new WishlistDTO
            {
                BookTitle = wishlist.Book?.Title,
                CustomerUsername = wishlist.Customer?.Username
            };
        }
    }
}
