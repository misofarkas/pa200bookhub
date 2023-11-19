using DataAccessLayer.Models;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Services;
using WebApplication1.Models;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/Books")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBookList()
        {
            var books = await _bookService.GetBooksAsync();
            if (books == null || books.Count == 0)
            {
                return NotFound("No books were found.");
            }
            return Ok(books);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBook(int id)
        {
            var book = await _bookService.GetBookAsync(id);

            if (book == null)
            {
                return NotFound($"No book with ID {id} was found.");
            }

            return Ok(book);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchBooks(string? title, string? description, decimal? price, string? genre, string? author)
        {
            var books = await _bookService.SearchBooksAsync(title, description, price, genre, author);
            if (books == null || books.Count == 0)
            {
                return NotFound("No books matching the search criteria were found.");
            }

            return Ok(books);
        }

        //[HttpPost]
        //public async Task<IActionResult> CreateBook(BookModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var book = new Book
        //    {
        //        Title = model.Title,
        //        AuthorId = model.AuthorId,
        //        GenreId = model.GenreId,
        //        PublisherId = model.PublisherId,
        //        Price = model.Price
        //    };

        //    _dbContext.Books.Add(book);
        //    await _dbContext.SaveChangesAsync();

        //    return Ok(book);
        //}

        //[HttpPut]
        //[Route("{id}")]
        //public async Task<IActionResult> UpdateBook(int id, BookModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var book = await _dbContext.Books.FindAsync(id);

        //    if (book == null)
        //    {
        //        return BadRequest($"No book with ID {id} was found.");
        //    }

        //    book.Title = model.Title;
        //    book.AuthorId = model.AuthorId;
        //    book.GenreId = model.GenreId;
        //    book.PublisherId = model.PublisherId;
        //    book.Price = model.Price;

        //    _dbContext.Books.Update(book);
        //    await _dbContext.SaveChangesAsync();

        //    return Ok(book);
        //}

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteBook(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
            {
                return NotFound($"No book with ID {id} was found.");
            }

            return NoContent();
        }
    }
}
