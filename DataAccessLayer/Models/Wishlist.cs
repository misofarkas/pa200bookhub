namespace DataAccessLayer.Models
{
    public class Wishlist : BaseEntity
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
    }

}
