using BusinessLayer.DTOs.Genre;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenreController : ControllerBase
    {
        private readonly IGenreService _genreService;

        public GenreController(IGenreService genreService)
        {
            _genreService = genreService;
        }

        [HttpGet]
        public async Task<IActionResult> GetGenres()
        {
            var genres = await _genreService.GetAllGenresAsync();
            return Ok(genres);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetGenre(int id)
        {
            var genre = await _genreService.GetGenreAsync(id);
            if (genre == null)
            {
                return NotFound($"No genre with ID {id} was found.");
            }
            return Ok(genre);
        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> SearchGenres(string name)
        {
            var genres = await _genreService.SearchGenres(name);
            return Ok(genres);
        }

        [HttpPost]
        public async Task<IActionResult> CreateGenre(GenreCreateUpdateDTO genre)
        {
            var result = await _genreService.CreateGenreAsync(genre);
            return Ok(result);
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateGenre(int id, GenreCreateUpdateDTO genre)
        {
            var result = await _genreService.UpdateGenreAsync(id, genre);
            if (result == null)
            {
                return NotFound($"No genre with ID {id} was found.");
            }
            return Ok(result);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteGenre(int id)
        {
            try
            {
                var result = await _genreService.DeleteGenreAsync(id);
                if (!result)
                {
                    return NotFound($"No genre with ID {id} was found.");
                }
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

    }
}
