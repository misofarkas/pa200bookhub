using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Book : BaseEntity
    {
        public string Title { get; set; }
        public virtual ICollection<Author> Authors { get; set; } = new List<Author>();
        public virtual ICollection<Genre> Genres { get; set; } = new List<Genre>();
        public int PublisherId { get; set; }
        public virtual Publisher Publisher { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
        public virtual ICollection<PurchaseHistory> PurchaseHistories { get; set; } = new List<PurchaseHistory>();

    }
}
