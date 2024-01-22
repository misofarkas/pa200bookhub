using BusinessLayer.DTOs.Enums;

namespace WebMVC.Models.Book
{
    public class BookSearchViewModel
    {
        public string Query { get; set; }
        public BookSearchField SearchIn { get; set; }
    }
}
