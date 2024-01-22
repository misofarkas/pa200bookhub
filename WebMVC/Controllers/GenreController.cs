using BusinessLayer.DTOs.Genre;
using BusinessLayer.Services;
using DataAccessLayer.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models;
using WebMVC.Models.Genre;

namespace WebMVC.Controllers
{
    [Route("genres")]
    [Authorize]
    public class GenreController : Controller
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet("search")]
        [AllowAnonymous]
        public async Task<IActionResult> Search(GenreSearchViewModel searchModel)
        {
            var result = await _genreService.SearchGenres(searchModel.Query ?? "");

            var viewModel = new GenreListViewModel
            {
                Genres = result.Adapt<IEnumerable<BasicGenreViewModel>>(),
            };

            return View("Index", viewModel);
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var genres = await _genreService.GetAllGenresAsync();
            var viewModel = new GenreListViewModel
            {
                Genres = genres.Adapt<IEnumerable<BasicGenreViewModel>>(),
            };
            return View(viewModel);
        }

        // GET: Genres/Create
        [HttpGet("create")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Genres/Create
        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(BasicGenreViewModel genreViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _genreService.CreateGenreAsync(genreViewModel.Adapt<GenreCreateUpdateDTO>()); // Add the genre
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    var model = new ErrorViewModel
                    {
                        RequestId = e.Message,
                    };
                    return View("Error", model);
                }
            }

            return View("Create", genreViewModel);
        }

        [HttpGet("edit/{id:int}")]
        public async Task<ActionResult> Edit(int id)
        {
            var genre = await _genreService.GetGenreAsync(id);
            if (genre == null)
            {
                var errorModel = new ErrorViewModel
                {
                    RequestId = "Genre not found",
                };
                return View("Error", errorModel);
            }

            return View(genre.Adapt<BasicGenreViewModel>());
        }

        [HttpPost("edit/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, BasicGenreViewModel genreViewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _genreService.UpdateGenreAsync(id, genreViewModel.Adapt<GenreCreateUpdateDTO>()); // Update the genre
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("", e.Message);
                    var model = new ErrorViewModel
                    {
                        RequestId = e.Message,
                    };
                    return View("Error", model);
                }
            }

            return View(genreViewModel);
        }

        [HttpPost("delete/{id:int}")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _genreService.DeleteGenreAsync(id); // Delete the genre
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ModelState.AddModelError("", e.Message);
                var model = new ErrorViewModel
                {
                    RequestId = e.Message,
                };
                return View("Error", model);
            }
        }
    }
}
