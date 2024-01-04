namespace WebMVC.Models.Book
{
    public class BookListViewModel
    {
        public IEnumerable<BookDetailViewModel> Books { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}
