using BusinessLayer.DTOs;
using BusinessLayer.Services;
using DataAccessLayer.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Models.Author;
using WebMVC.Models.Book;
using WebMVC.Models.Genre;


namespace WebMVC.Controllers
{
    [Route("search")]
    [Authorize]
    public class SearchController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IGenreService _genreService;
        private readonly IAuthorService _authorService;

        public SearchController(IBookService bookService, IGenreService genreService, IAuthorService authorService)
        {
            _bookService = bookService;
            _authorService = authorService;
            _genreService = genreService;
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search(GeneralSearchViewModel searchModel)
        {
            var genres = await _genreService.SearchGenres(searchModel.Query);
            var books = await _bookService.SearchBooksAsync(searchModel.Query, null, null, null, null);
            var authors = await _authorService.SearchAuthor(searchModel.Query);

            var viewModel = new SearchListViewModel
            {
                Genres = genres.Adapt<IEnumerable<BasicGenreViewModel>>(),
                Books = books.Adapt<IEnumerable<BookDetailViewModel>>(),
                Authors = authors.Adapt<IEnumerable<BasicAuthorViewModel>>(),
            };

            return View("SearchResult", viewModel);
        }
    }
}
