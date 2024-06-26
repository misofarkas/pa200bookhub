﻿using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Book;
using BusinessLayer.Services;
using DataAccessLayer.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebMVC.Models;
using WebMVC.Models.Book;

namespace WebMVC.Controllers
{
    [Route("books")]
    [Authorize]
    public class BookController : Controller
    {
        private readonly IBookService _bookService;
        private readonly IAuthorService _authorService;
        private readonly IGenreService _genreService;
        private readonly IPublisherService _publisherService;
        private readonly UserManager<LocalIdentityUser> _userManager;
        private readonly IMemoryCache _memoryCache;

        public BookController(IBookService bookService, IAuthorService authorService, IGenreService genreService, IPublisherService publisherService, UserManager<LocalIdentityUser> userManager, IMemoryCache memoryCache)
        {
            _bookService = bookService;
            _authorService = authorService;
            _genreService = genreService;
            _userManager = userManager;
            _publisherService = publisherService;
            _memoryCache = memoryCache;
        }

        [HttpGet]
        public async Task<IActionResult> Index(BookSearchViewModel searchModel, int page = 1, int pageSize = 10)
        {
            BookSearchCriteriaDTO searchCriteria = searchModel.Adapt<BookSearchCriteriaDTO>();

            if (searchCriteria.Query == null)
            {
                searchCriteria.Query = "";
            }

            var result = await _bookService.SearchBooksWithCriteria(searchCriteria, page, pageSize);

            var viewModel = new SearchBookListViewModel
            {
                Books = result.Items.Adapt<IEnumerable<BookDetailViewModel>>(),
                CurrentPage = page,
                TotalPages = (int)Math.Ceiling((double)result.TotalCount / pageSize),
                SearchCriteria = searchModel,
            };

            return View("Index", viewModel);
        }

        [HttpGet("create")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create()
        {
            ViewBag.Authors = await _authorService.GetAll();
            ViewBag.Genres = await _genreService.GetAllGenresAsync();
            ViewBag.Publishers = await _publisherService.GetAllPublishersAsync();

            return View();
        }

        [HttpPost("create")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookCreateUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _bookService.CreateBookAsync(model.Adapt<BookCreateUpdateDTO>());
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    var ErrorModel = new ErrorViewModel
                    {
                        RequestId = e.Message,
                    };
                    return View("Error", ErrorModel);
                }

            }
            var errorModel = new ErrorViewModel
            {
                RequestId = "Invalid model state",
            };
            return View("Error", errorModel);
        }

        [HttpGet("edit/{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var book = await _bookService.GetBookAsync(id);
            if (book == null)
            {
                return NotFound($"No book with ID {id} was found.");
            }
            ViewBag.Authors = await _authorService.GetAll();
            ViewBag.Genres = await _genreService.GetAllGenresAsync();
            ViewBag.Publishers = await _publisherService.GetAllPublishersAsync();
            ViewBag.PriceValue = book.Price.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture);

            var model = new BookCreateUpdateViewModel
            {
                Title = book.Title,
                Description = book.Description,
                Price = book.Price,
                PublisherId = book.Publisher.Id,
                PrimaryGenreId = book.PrimaryGenre.Id,
                AuthorIds = book.Authors.Select(ab => ab.Id).ToList(),
                GenreIds = book.Genres.Select(g => g.Id).ToList()
            };

            return View(model);
        }

        [HttpPost("edit/{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookCreateUpdateViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _bookService.UpdateBookAsync(id, model.Adapt<BookCreateUpdateDTO>());
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    var ErrorModel = new ErrorViewModel
                    {
                        RequestId = e.Message,
                    };
                    return View("Error", ErrorModel);
                }

            }
            var errorModel = new ErrorViewModel
            {
                RequestId = "Invalid model state",
            };
            return View("Error", errorModel);
        }

        [HttpPost("delete/{id}")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _bookService.DeleteBookAsync(id);
            if (!result)
            {
                var errorModel = new ErrorViewModel
                {
                    RequestId = $"No book with ID {id} was found.",
                };
                return View("Error", errorModel);
            }
            return RedirectToAction("Index");
        }

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            string cacheKey = $"book-{id}";
            if (!_memoryCache.TryGetValue(cacheKey, out BookDetailViewModel viewModel))
            {
                var book = await _bookService.GetBookAsync(id);
                if (book == null)
                {
                    var errorModel = new ErrorViewModel
                    {
                        RequestId = $"No book with ID {id} was found.",
                    };
                    return View("Error", errorModel);
                }
                viewModel = book.Adapt<BookDetailViewModel>();
                _memoryCache.Set(cacheKey, viewModel, TimeSpan.FromMinutes(10));
            }
            return View(viewModel);
        }
    }
}
