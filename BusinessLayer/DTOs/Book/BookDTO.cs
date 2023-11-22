using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTOs.Author;

namespace BusinessLayer.DTOs.Book
{
    public class BookDTO
    {
        public string Title { get; set; }
        public List<AuthorWithoutBooksDTO> Authors { get; set; }
        public List<GenreDTO> Genres { get; set; }
        public PublisherDTO Publisher { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public List<ReviewDTO> Reviews { get; set; }
    }

}
