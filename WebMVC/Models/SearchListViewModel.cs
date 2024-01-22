using WebMVC.Models.Author;
using WebMVC.Models.Genre;
using WebMVC.Models.Book;

namespace WebMVC.Models
{
    public class SearchListViewModel
    {
        public IEnumerable<BookDetailViewModel> Books { get; set; }
        public IEnumerable<BasicGenreViewModel> Genres { get; set; }
        public IEnumerable<BasicAuthorViewModel> Authors { get; set; }
    }
}
