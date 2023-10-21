using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Genre : BaseEntity
    {
        public string Name { get; set; }
        public virtual ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
