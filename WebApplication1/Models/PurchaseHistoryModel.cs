using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class PurchaseHistoryModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public string CustomerUsername { get; set; }
        public DateTime PurchaseDate { get; set; }

    }
}
