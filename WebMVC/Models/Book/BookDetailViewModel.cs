using System.ComponentModel.DataAnnotations;
using System.Reflection;
using WebMVC.Models.Author;
using WebMVC.Models.Genre;
using WebMVC.Models.NewFolder;

namespace WebMVC.Models.Book
{
    public class BookDetailViewModel
    {
        public int Id { get; set; }
        public required string Title { get; set; }
        public string Description { get; set; }
        public required decimal Price { get; set; }

        public BasicGenreViewModel PrimaryGenre { get; set; }

        public IEnumerable<BasicAuthorViewModel> Authors { get; set; }
        public IEnumerable<BasicGenreViewModel> Genres { get; set; }
        public BasicPublisherViewModel Publisher { get; set; }
    }
}
