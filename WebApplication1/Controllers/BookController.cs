using DataAccessLayer.Models;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Services;
using System.Threading.Tasks;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.Enums;

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
        public async Task<IActionResult> GetBookList(string? format)
        {
            var books = await _bookService.GetBooksAsync();
            if (books == null || books.Count == 0)
            {
                return NotFound("No books were found.");
            }
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> CreateBook(BookDTO model)
        {
            var result = await _bookService.CreateBookAsync(model);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetBook(int id, string? format)
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
        public async Task<IActionResult> SearchBooks(string? title, string? description, decimal? price, string? genre, string? author, string format = "json")
        {
            var books = await _bookService.SearchBooksAsync(title, description, price, genre, author);
            if (books == null || books.Count == 0)
            {
                return NotFound("No books matching the search criteria were found.");
            }

            return Ok(books);
        }

        [HttpGet]
        [Route("searchCriteria")]
        public async Task<IActionResult> SearchBooksWithCriteria(string query, BookSearchField searchIn, int page, int pageSize, string format = "json")
        {
            BookSearchCriteriaDTO searchCriteria = new BookSearchCriteriaDTO { Query = query, SearchIn = searchIn };
            var books = await _bookService.SearchBooksWithCriteria(searchCriteria, page, pageSize);
            if (books == null || books.TotalCount == 0)
            {
                return NotFound("No books matching the search criteria were found.");
            }

            return Ok(books);
        }

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

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateBook(int id, BookUpdateDTO model)
        {
            var result = await _bookService.UpdateBookAsync(id, model);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}
