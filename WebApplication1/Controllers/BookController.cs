using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly BookHubDBContext _dbContext;

        public BookController(BookHubDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetBookList()
        {
            var books = await _dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Select(b => new BookModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorId = b.AuthorId,
                    AuthorName = b.Author.Name,
                    GenreId = b.GenreId,
                    PublisherId = b.PublisherId,
                    PublisherName = b.Publisher.Name,
                    Price = b.Price
                })
                .ToListAsync();

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
            var book = await _dbContext.Books
                .Include(b => b.Author)
                .Include(b => b.Publisher)
                .Where(b => b.Id == id)
                .Select(b => new BookModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorId = b.AuthorId,
                    AuthorName = b.Author.Name,
                    GenreId = b.GenreId,
                    PublisherId = b.PublisherId,
                    PublisherName = b.Publisher.Name,
                    Price = b.Price
                })
                .FirstOrDefaultAsync();

            if (book == null)
            {
                return BadRequest($"No book with ID {id} was found.");
            }

            return Ok(book);
        }

        [HttpPost]
        [Route("create")]
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
        [Route("{id}/update")]
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
        [Route("{id}/delete")]
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
