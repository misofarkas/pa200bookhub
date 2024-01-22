using System.ComponentModel.DataAnnotations;
using WebMVC.Models.Author;
using WebMVC.Models.Genre;
using WebMVC.Models.Publisher;

namespace WebMVC.Models.Book
{
    public class BookCreateUpdateViewModel
    {
        public int Id { get; set; }
        [Required]
        public required string Title { get; set; }
        [Required(ErrorMessage = "Description is required")]
        public required string Description { get; set; }

        [Required]
        public int PrimaryGenreId { get; set; }

        [Required]
        [Range(0, Double.MaxValue, ErrorMessage = "Please enter a valid price")]
        public required decimal Price { get; set; }

        [Required(ErrorMessage = "Please select at least one author.")]
        public IEnumerable<int> AuthorIds { get; set; }
        [Required(ErrorMessage = "Please select at least one genre.")]
        public IEnumerable<int> GenreIds { get; set; }
        [Required]
        public int PublisherId { get; set; }
    }
}
