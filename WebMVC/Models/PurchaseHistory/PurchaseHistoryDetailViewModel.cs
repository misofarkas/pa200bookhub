namespace WebMVC.Models.PurchaseHistory
{
    public class PurchaseHistoryDetailViewModel
    {
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public string CustomerUsername { get; set; }
        public DateTime PurchaseDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Paid { get; set; }
    }
}
