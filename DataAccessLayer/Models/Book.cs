using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public int GenreId { get; set; }
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public decimal Price { get; set; }
    }
}
