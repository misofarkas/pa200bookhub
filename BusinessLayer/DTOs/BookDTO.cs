using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class BookDTO
    {
        public string Title { get; set; }
        public List<AuthorDTO> Authors { get; set; }
        public List<GenreDTO> Genres { get; set; }
        public string PublisherName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
    }

}
