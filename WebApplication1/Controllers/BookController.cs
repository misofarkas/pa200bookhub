using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Services;
using WebApplication1.Models;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        [Route("list")]
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
        //[Route("create")]
        //public async Task<IActionResult> CreateBook([FromBody] BookModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var book = await _bookService.CreateBookAsync(model);
        //    return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
        //}

        //[HttpPut]
        //[Route("{id}/update")]
        //public async Task<IActionResult> UpdateBook(int id, [FromBody] BookModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    var updatedBook = await _bookService.UpdateBookAsync(id, model);
        //    if (updatedBook == null)
        //    {
        //        return NotFound($"No book with ID {id} was found.");
        //    }

        //    return Ok(updatedBook);
        //}

        [HttpDelete]
        [Route("{id}/delete")]
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

