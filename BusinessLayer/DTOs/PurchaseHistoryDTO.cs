using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class PurchaseHistoryDTO
    {
        public string BookTitle { get; set; }
        public string CustomerUsername { get; set; }
        public DateTime PurchaseDate { get; set; }
    }

}
