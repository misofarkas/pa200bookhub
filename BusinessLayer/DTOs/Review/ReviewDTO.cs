using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs.Review
{
    public class ReviewDTO : BaseDTO
    {
        public string BookTitle { get; set; }
        public string CustomerUsername { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
    }

}
