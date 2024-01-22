using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.Genre;
using BusinessLayer.DTOs.Publisher;
using BusinessLayer.DTOs.PurchaseHistory;
using BusinessLayer.DTOs.Review;
using DataAccessLayer.Models;
using Mapster;
using System.Linq;

namespace BusinessLayer.Mapper
{
    public static class EntityMapper
    {
        public static Author MapToAuthor(this AuthorDTO authorDTO)
        {
            return authorDTO.Adapt<Author>();
        }

        public static Author MapToAuthor(this AuthorWithoutBooksDTO authorDTOList)
        {
            return authorDTOList.Adapt<Author>();
        }

        public static Author MapToAuthor(this AuthorCreateUpdateDTO authorDTOList)
        {
            return authorDTOList.Adapt<Author>();
        }


        public static Book MapToBook(this BookDTO bookDTO)
        {
            return bookDTO.Adapt<Book>();
        }

        public static Book MapToBook(this BookCreateUpdateDTO bookDTO)
        {
            return bookDTO.Adapt<Book>();
        }

        public static Customer MapToCustomer(this CustomerDTO customerDTO)
        {
            return customerDTO.Adapt<Customer>();
        }

        public static Genre MapToGenre(this GenreDTO genreDTO)
        {
            return genreDTO.Adapt<Genre>();
        }
        public static Genre MapToGenre(this GenreCreateUpdateDTO genreDTO)
        {
            return genreDTO.Adapt<Genre>();
        }

        public static Review MapToReview(this ReviewBasicDTO reviewDTO)
        {
            return reviewDTO.Adapt<Review>();
        }

        public static Review MapToReview(this ReviewCreateUpdateDTO reviewDTO)
        {
            return reviewDTO.Adapt<Review>();
        }

        public static Publisher MapToPublisher(this PublisherDTO publisherDTO)
        {
            return publisherDTO.Adapt<Publisher>();
        }

        public static Publisher MapToPublisher(this PublisherCreateUpdateDTO publisherDTO)
        {
            return publisherDTO.Adapt<Publisher>();
        }

        public static PurchaseHistory MapToPurchaseHistory(this PurchaseHistoryDTO purchaseHistoryDTO)
        {
            return purchaseHistoryDTO.Adapt<PurchaseHistory>();
        }

        public static PurchaseHistory MapToPurchaseHistory(this PurchaseHistoryCreateDTO purchaseHistoryDTO)
        {
            return purchaseHistoryDTO.Adapt<PurchaseHistory>();
        }
    }
}
