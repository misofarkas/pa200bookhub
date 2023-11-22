using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs.Book;
using DataAccessLayer.Models;
using Mapster;
using System.Linq;

namespace BusinessLayer.Mapper
{
    public static class DTOMapper
    {
        public static AuthorDTO MapToAuthorDTO(this Author author)
        {
            return author.Adapt<AuthorDTO>();
        }

        public static AuthorWithoutBooksDTO MapToAuthorDTOList(this Author author)
        {
            return author.Adapt<AuthorWithoutBooksDTO>();
        }

        public static BookDTO MapToBookDTO(this Book book)
        {
            return book.Adapt<BookDTO>();
        }
        public static BookWithoutAuthorDTO MapToBookWithoutAuthorDTO(this Book book)
        {
            return book.Adapt<BookWithoutAuthorDTO>();
        }

        public static CustomerDTO MapToCustomerDTO(this Customer customer)
        {
            return customer.Adapt<CustomerDTO>();
        }

        public static GenreDTO MapToGenreDTO(this Genre genre)
        {
            return genre.Adapt<GenreDTO>();
        }

        public static PublisherDTO MapToPublisherDTO(this Publisher publisher)
        {
            return publisher.Adapt<PublisherDTO>();
        }

        public static PurchaseHistoryDTO MapToPurchaseHistoryDTO(this PurchaseHistory purchaseHistory)
        {
            return purchaseHistory.Adapt<PurchaseHistoryDTO>();
        }

        public static ReviewDTO MapToReviewDTO(this Review review)
        {
            return review.Adapt<ReviewDTO>();
        }

        public static WishlistDTO MapToWishlistDTO(this Wishlist wishlist)
        {
            return wishlist.Adapt<WishlistDTO>();
        }
    }
}
