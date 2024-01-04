using BusinessLayer.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.DTOs
{
    public class BookSearchCriteriaDTO
    {
        public string Query { get; set; }
        public BookSearchField SearchIn { get; set; }
    }
}
