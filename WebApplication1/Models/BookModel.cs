using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class BookModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Title { get; set; }
        [JsonIgnore]
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        [JsonIgnore]
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        [JsonIgnore]
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public virtual ICollection<ReviewModel> Reviews { get; set; } = new List<ReviewModel>();
        public virtual ICollection<WishListModel> Wishlists { get; set; } = new List<WishListModel>();
        public virtual ICollection<PurchaseHistoryModel> PurchaseHistories { get; set; } = new List<PurchaseHistoryModel>();
    }
}

