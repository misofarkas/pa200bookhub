using WebMVC.Models.Genre;

namespace WebMVC.Models.PurchaseHistory
{
    public class PurchaseHistoryListViewModel
    {
        public IEnumerable<PurchaseHistoryDetailViewModel> PurchaseHistories { get; set; }
    }
}
