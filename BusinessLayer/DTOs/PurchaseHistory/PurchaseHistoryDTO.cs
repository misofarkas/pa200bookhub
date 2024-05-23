using BusinessLayer.DTOs.Book;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.PurchaseHistory
{
    public class PurchaseHistoryDTO : BaseDTO
    {
        public BookDTO Book { get; set; }
        public CustomerDTO Customer { get; set; }
        public DateTimeOffset PurchaseDate { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Paid { get; set; }
    }

}
