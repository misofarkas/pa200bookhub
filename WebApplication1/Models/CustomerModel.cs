using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class CustomerModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public virtual ICollection<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();
        public virtual ICollection<WishListModel> Wishlists { get; set; } = new List<WishListModel>();
        public virtual ICollection<PurchaseHistoryModel> PurchaseHistories { get; set; } = new List<PurchaseHistoryModel>();
    }
}
