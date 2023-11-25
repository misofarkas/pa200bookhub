using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs.Book;
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

        public static Book MapToBook(this BookWithoutAuthorDTO bookDTO)
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

        public static Publisher MapToPublisher(this PublisherDTO publisherDTO)
        {
            return publisherDTO.Adapt<Publisher>();
        }
    }
}
