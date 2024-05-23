namespace DataAccessLayer.Models
{
    public class PurchaseHistory : BaseEntity
    {
        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int CustomerId { get; set; }
        public virtual Customer Customer { get; set; }
        public DateTimeOffset PurchaseDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Paid { get; set; }
    }

}
