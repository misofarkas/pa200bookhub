namespace DataAccessLayer.Models
{
    public class PurchaseHistory : BaseEntity
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTime PurchaseDate { get; set; }
    }

}
