using System.Collections.Generic;

namespace DataAccessLayer.Models
{
    public class Customer : BaseEntity
    {
        public string Username { get; set; }
        public bool isDeleted { get; set; } = false;
        public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();
        public virtual ICollection<Wishlist> Wishlists { get; set; } = new List<Wishlist>();
        public virtual ICollection<PurchaseHistory> PurchaseHistories { get; set; } = new List<PurchaseHistory>();
    }
}
