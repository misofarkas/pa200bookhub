using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.PurchaseHistory
{
    public class PurchaseHistoryUpdateDTO
    {
        public DateTime PurchaseDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Paid { get; set; }

    }
}
