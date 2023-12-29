// File: WebMVC/Controllers/AdminController.cs
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models; // Adjust namespace based on your setup
using BusinessLayer.Services; // Ensure BusinessLayer is referenced
using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs;

namespace WebMVC.Controllers
{
    [Route("admin")]
    public class AdminController : Controller
    {
        private readonly IBookService _bookService;
        private readonly ICustomerService _customerService;
        private readonly IGenreService _genreService;

        public AdminController(IBookService bookService, ICustomerService customerService,  IGenreService genreService)
        {
            _bookService = bookService;
            _customerService = customerService;
            _genreService = genreService;
        }

        [HttpGet("edit-book/{id}")]
        public async Task<IActionResult> EditBook(int id)
        {
            var book = await _bookService.GetBookAsync(id);
            if (book == null) return NotFound();

            return View(book); // Create corresponding view
        }

        [HttpPost("edit-book/{id}")]
        public async Task<IActionResult> EditBook(int id, BookUpdateDTO book)
        {
            await _bookService.UpdateBookAsync(id, book);
            return RedirectToAction("Index", "Home"); // or any other admin page
        }

        [HttpGet("edit-customer/{id}")]
        public async Task<IActionResult> EditCustomer(int id)
        {
            var customer = await _customerService.GetCustomerAsync(id);
            if (customer == null) return NotFound();

            return View(customer); // Create corresponding view
        }

        [HttpPost("edit-customer/{id}")]
        public async Task<IActionResult> EditCustomer(int id, CustomerDTO customer)
        {
            await _customerService.UpdateCustomerAsync(id, customer);
            return RedirectToAction("Index", "Home"); // or any other admin page
        }

        [HttpGet("edit-genre/{id}")]
        public async Task<IActionResult> EditGenre(int id)
        {
            var genre = await _genreService.GetGenreAsync(id);
            if (genre == null) return NotFound();

            return View(genre); // Create corresponding view
        }

        [HttpPost("edit-genre/{id}")]
        public async Task<IActionResult> EditGenre(int id, GenreDTO genre)
        {
            await _genreService.UpdateGenreAsync(id, genre);
            return RedirectToAction("Index", "Admin"); // or any other admin page
        }
    }
}
