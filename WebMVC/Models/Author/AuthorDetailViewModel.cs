using WebMVC.Models.Book;

namespace WebMVC.Models.Author
{
    public class AuthorDetailViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<BookDetailViewModel> Books { get; set; }
    }
}
