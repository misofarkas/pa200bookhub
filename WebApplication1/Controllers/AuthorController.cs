using DataAccessLayer.Models;
using DataAccessLayer.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BusinessLayer.Services.Author;
using BusinessLayer.DTOs.Author;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/Authors")]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorService _authorService;

        public AuthorController(IAuthorService authorService)
        {
            _authorService = authorService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAuthorList(string? format)
        {
            var authors = await _authorService.GetAll();
            if (authors == null || authors.Count == 0)
            {
                return NotFound("No Authors were found");
            }
            return Ok(authors);

        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAuthor(string? format, int id)
        {
            var author = await _authorService.GetByAuthorId(id);
            if (author == null)
            {
                return NotFound("No Author with this id has been found");
            }
            return Ok(author);

        }

        [HttpGet]
        [Route("search")]
        public async Task<IActionResult> GetAuthorByName(string? format, string name)
        {
            var author = await _authorService.GetByAuthorName(name);
            if (author == null)
            {
                return NotFound("No Author with this name has been found");
            }
            return Ok(author);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAuthor (AuthorWithoutBooksDTO author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var success = await _authorService.CreateAuthor(author);
            if (success)
            {
                return Ok("Author created");
            }
            return BadRequest("An error occured while creating new author");
            
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorWithoutBooksDTO model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var success = await _authorService.UpdateAuthor(model);
            if (success)
            {
                return Ok("Author updated");
            }
            return BadRequest("An error occured while updating author");

        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var result = await _authorService.DeleteAuthor(id);
            if (result)
            {
                return NoContent();
            }
            return BadRequest("No Author with this ID has been found or the author has still linked books.");
        }
    }
}


