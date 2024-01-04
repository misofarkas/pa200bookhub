using BusinessLayer.DTOs;
using BusinessLayer.Services;
using DataAccessLayer.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models.Book;


namespace WebMVC.Controllers
{
    [Route("books")]
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly UserManager<LocalIdentityUser> _userManager;

        public BookController(IBookService bookService, UserManager<LocalIdentityUser> userManager)
        {
            _bookService = bookService;
            _userManager = userManager;
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search(BookSearchViewModel searchModel, int page = 1, int pageSize = 10)
        {
            BookSearchCriteriaDTO searchCriteria = searchModel.Adapt<BookSearchCriteriaDTO>();
            var result = await _bookService.SearchBooksWithCriteria(searchCriteria, page, pageSize);

            var viewModel = new SearchBookListViewModel
            {
                Books = result.Items.Adapt<IEnumerable<BookDetailViewModel>>(),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize),
                SearchCriteria = searchModel,
            };

            return View("BookSearchResult", viewModel);
        }
    }
}
