using BusinessLayer.Services;
using DataAccessLayer.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
            var result = await _genreService.SearchGenres(searchModel.Query);

            var viewModel = new GenreListViewModel
            {
                Genres = result.Adapt<IEnumerable<BasicGenreViewModel>>(),
            };

            return View("GenreSearchResult", viewModel);
        }
    }
}
