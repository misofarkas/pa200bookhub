using System.Reflection;
using WebMVC.Models.Author;
using WebMVC.Models.Genre;
namespace WebMVC.Models.Book
{
    public class BookDetailViewModel
    {
        public required string Title { get; set; }
        public string Description { get; set; }

        public IEnumerable<BasicAuthorViewModel> Authors { get; set; }
        public IEnumerable<BasicGenreViewModel> Genres { get; set; }
        public string Publisher { get; set; }
    }
}
