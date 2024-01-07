using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessLayer.DTOs.Book;

namespace BusinessLayer.DTOs.Author
{
    public class AuthorDTO : AuthorWithoutBooksDTO
    {
        public virtual ICollection<BookWithoutAuthorDTO> Books { get; set; }
    }

}
