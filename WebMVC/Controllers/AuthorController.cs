using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs.Book;
using BusinessLayer.Services;
using DataAccessLayer.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using WebMVC.Models;
using WebMVC.Models.Author;
using WebMVC.Models.Book;

namespace WebMVC.Controllers
{
    [Route("authors")]
    [Authorize]
    public class AuthorController : Controller
    {
        private readonly IAuthorService _authorService;
        private readonly IMemoryCache _memoryCache;

        public AuthorController(IAuthorService authorService, IMemoryCache memoryCache)
        {
            _authorService = authorService;
            _memoryCache = memoryCache;
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search(AuthorSearchViewModel searchModel)
        {
            var result = await _authorService.SearchAuthor(searchModel.Query ?? "");

            var viewModel = new AuthorListViewModel
            {
                Authors = result.Adapt<IEnumerable<BasicAuthorViewModel>>(),
            };

            return View("Index", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var authors = await _authorService.GetAll();
            var viewModel = new AuthorListViewModel
            {
                Authors = authors.Adapt<IEnumerable<BasicAuthorViewModel>>(),
            };
            return View(viewModel);
        }

        [HttpGet("create")]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BasicAuthorViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _authorService.CreateAuthor(model.Adapt<AuthorCreateUpdateDTO>());
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
        public async Task<IActionResult> Edit(int id)
        {
            var author = await _authorService.GetByAuthorId(id);
            if (author == null)
            {
                return NotFound("No Author with this id has been found");
            }
            return View(author.Adapt<BasicAuthorViewModel>());
        }

        [HttpPost("edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BasicAuthorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            try
            {
                var result = await _authorService.UpdateAuthor(id, model.Adapt<AuthorCreateUpdateDTO>());
                return RedirectToAction("Index");
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

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                var result = await _authorService.DeleteAuthor(id);
                return RedirectToAction("Index");
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

        [HttpGet("details/{id}")]
        public async Task<IActionResult> Details(int id)
        {
            string cacheKey = $"author-{id}";
            if (!_memoryCache.TryGetValue(cacheKey, out AuthorDetailViewModel viewModel))
            {
                var author = await _authorService.GetByAuthorId(id);
                if (author is null)
                {
                    var errorModel = new ErrorViewModel
                    {
                        RequestId = $"No book with ID {id} was found.",
                    };
                    return View("Error", errorModel);
                }

                viewModel = author.Adapt<AuthorDetailViewModel>();
                _memoryCache.Set(cacheKey, viewModel, TimeSpan.FromMinutes(10));
            }
            return View(viewModel);
        }
    }
}
