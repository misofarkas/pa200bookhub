using System.Text.Json.Serialization;

namespace WebApplication1.Models
{
    public class AuthorModel
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<BookModel> Books { get; set; }
    }
}

