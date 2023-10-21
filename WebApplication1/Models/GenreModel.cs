using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class GenreModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
