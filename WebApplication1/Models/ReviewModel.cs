using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class ReviewModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string BookTitle { get; set; }
        public string CustomerUsername { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }

    }
}
