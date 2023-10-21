using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public virtual Author Author { get; set; }
        public int GenreId { get; set; }
        public virtual Genre Genre { get; set; }
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public decimal Price { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
        public virtual ICollection<PurchaseHistory> PurchaseHistories { get; set; } = new List<PurchaseHistory>();

    }
}
