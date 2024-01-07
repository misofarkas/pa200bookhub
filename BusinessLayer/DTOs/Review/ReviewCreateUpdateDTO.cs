using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Review
{
    public class ReviewCreateUpdateDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }
}
