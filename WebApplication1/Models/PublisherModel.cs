using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class PublisherModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}

