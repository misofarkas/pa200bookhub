using BusinessLayer.DTOs.Publisher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.PurchaseHistory
{
    public class PurchaseHistoryCreateDTO
    {
        [JsonIgnore]
        public int Id { get; set; }
        public int BookId { get; set; }
        public int CustomerId { get; set; }

        [JsonIgnore]
        public DateTimeOffset PurchaseDate { get; set; }

        [JsonIgnore]
        public decimal TotalPrice { get; set; }

        [JsonIgnore]
        public bool Paid { get; set;}

    }
}
