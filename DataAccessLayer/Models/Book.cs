using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public virtual ICollection<AuthorBook> AuthorBooks { get; set; } = new List<AuthorBook>();
        public virtual ICollection<GenreBook> GenreBooks { get; set; } = new List<GenreBook>();
        public int PrimaryGenreId { get; set; }
        public virtual Genre PrimaryGenre { get; set; }
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

        public virtual ICollection<PurchaseHistory> PurchaseHistories { get; set; } = new List<PurchaseHistory>();

    }
}
