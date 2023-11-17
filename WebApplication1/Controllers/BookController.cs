using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using static System.Reflection.Metadata.BlobBuilder;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/Books")]
    public class BookController : ControllerBase
    {
        private readonly BookHubDBContext _dbContext;

        public BookController(BookHubDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        private async Task<List<BookModel>> GetBooksCommonQuery(IQueryable<Book> query)
        {
            return await query
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Include(b => b.Reviews)
                .Include(b => b.Wishlists)
                .Include(b => b.PurchaseHistories)
                .Select(b => new BookModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorName = b.Author.Name,
                    GenreName = b.Genre.Name,
                    PublisherName = b.Publisher.Name,
                    Price = b.Price,
                    Description = b.Description,
                    Reviews = b.Reviews.Select(w => new ReviewModel
                    {
                        Id = w.Id,
                        CustomerUsername = w.Customer.Username,
                        BookTitle = w.Book.Title,
                        Rating = w.Rating,
                        Comment = w.Comment
                    }).ToList(),
                    PurchaseHistories = b.PurchaseHistories.Select(w => new PurchaseHistoryModel
                    {
                        Id = w.Id,
                        BookTitle = w.Book.Title,
                        CustomerUsername = w.Customer.Username,
                        PurchaseDate = w.PurchaseDate
                    }).ToList(),
                    Wishlists = b.Wishlists.Select(w => new WishListModel
                    {
                        Id = w.Id,
                        CustomerName = w.Customer.Username
                    }).ToList()
                })
                .ToListAsync();
        }

        [HttpGet]
        public async Task<IActionResult> GetBookList()
        {
            var books = await GetBooksCommonQuery(_dbContext.Books);

            if (books == null || !books.Any())
            {
                return BadRequest("No books were found.");
            }

            return Ok(books);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await GetBooksCommonQuery(_dbContext.Books.Where(b => b.Id == id));

            if (book == null || !book.Any())
            {
                return BadRequest($"No book with ID {id} was found.");
            }

            return Ok(book);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchBooks(string? title, string? description, decimal? price, string? genre, string? author)
        {
            if (string.IsNullOrWhiteSpace(title) && string.IsNullOrWhiteSpace(description) && !price.HasValue
                && string.IsNullOrWhiteSpace(genre) && string.IsNullOrWhiteSpace(author))
            {
                return BadRequest("Please provide a search input.");
            }

            var query = _dbContext.Books.AsQueryable();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(b => b.Title.Contains(title));
            }

            if (!string.IsNullOrWhiteSpace(description))
            {
                query = query.Where(b => b.Description.Contains(description));
            }

            if (price.HasValue)
            {
                query = query.Where(b => b.Price == price);
            }

            if (!string.IsNullOrWhiteSpace(genre))
            {
                query = query.Where(b => b.Genre.Name.Contains(genre));
            }

            if (!string.IsNullOrWhiteSpace(author))
            {
                query = query.Where(b => b.Author.Name.Contains(author));
            }

            var books = await GetBooksCommonQuery(query);

            if (books == null || !books.Any())
            {
                return NotFound("No books matching the search criteria were found.");
            }

            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(BookModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = new Book
            {
                Title = model.Title,
                AuthorId = model.AuthorId,
                GenreId = model.GenreId,
                PublisherId = model.PublisherId,
                Price = model.Price
            };

            _dbContext.Books.Add(book);
            await _dbContext.SaveChangesAsync();

            return Ok(book);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var book = await _dbContext.Books.FindAsync(id);

            if (book == null)
            {
                return BadRequest($"No book with ID {id} was found.");
            }

            book.Title = model.Title;
            book.AuthorId = model.AuthorId;
            book.GenreId = model.GenreId;
            book.PublisherId = model.PublisherId;
            book.Price = model.Price;

            _dbContext.Books.Update(book);
            await _dbContext.SaveChangesAsync();

            return Ok(book);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);

            if (book == null)
            {
                return BadRequest($"No book with ID {id} was found.");
            }

            _dbContext.Books.Remove(book);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}
