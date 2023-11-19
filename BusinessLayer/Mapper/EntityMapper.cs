using BusinessLayer.DTOs;
using DataAccessLayer.Models;
using System.Linq;

namespace BusinessLayer.Mapper
{
    public static class EntityMapper
    {
        public static Author MapToAuthor(this AuthorDTO authorDTO)
        {
            return new Author
            {
                Name = authorDTO.Name
            };
        }

        public static Book MapToBook(this BookDTO bookDTO)
        {
            return new Book
            {
                Title = bookDTO.Title,
                Price = bookDTO.Price,
                Description = bookDTO.Description
            };
        }

        public static Customer MapToCustomer(this CustomerDTO customerDTO)
        {
            return new Customer
            {
                Username = customerDTO.Username
            };
        }

        public static Genre MapToGenre(this GenreDTO genreDTO)
        {
            return new Genre
            {
                Name = genreDTO.Name
            };
        }

        public static Publisher MapToPublisher(this PublisherDTO publisherDTO)
        {
            return new Publisher
            {
                Name = publisherDTO.Name
            };
        }
    }
}
