using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class WishListModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string CustomerName { get; set; }

    }
}
