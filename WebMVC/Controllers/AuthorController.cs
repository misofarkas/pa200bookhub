using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs.Book;
using BusinessLayer.Services;
using DataAccessLayer.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search(AuthorSearchViewModel searchModel)
        {
            var result = await _authorService.SearchAuthor(searchModel.Query);

            var viewModel = new AuthorListViewModel
            {
                Authors = result.Adapt<IEnumerable<BasicAuthorViewModel>>(),
            };

            return View("AuthorSearchResult", viewModel);
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
            var result = await _authorService.DeleteAuthor(id);
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
            var author = await _authorService.GetByAuthorId(id);
            if (author is null)
            {
                var errorModel = new ErrorViewModel
                {
                    RequestId = $"No book with ID {id} was found.",
                };
                return View("Error", errorModel);
            }

            var viewModel = author.Adapt<AuthorDetailViewModel>();
            return View(viewModel);
        }
    }
}
