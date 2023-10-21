namespace WebApplication1.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public int GenreId { get; set; }
        public string GenreName { get; set; }
        public int PublisherId { get; set; }
        public string PublisherName { get; set; }
        public decimal Price { get; set; }
    }
}

