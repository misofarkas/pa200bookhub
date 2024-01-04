using BusinessLayer.Services;
using DataAccessLayer.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Models.Author;

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
    }
}
