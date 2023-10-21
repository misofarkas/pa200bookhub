using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthorController : ControllerBase
    {
        private readonly BookHubDBContext _dbContext;

        public AuthorController(BookHubDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("list")]
        public async Task<IActionResult> GetAuthorList()
        {

            var authors = await _dbContext.Authors.Include(b => b.Books).Select(b => new AuthorModel
            {
                Id = b.Id,
                Name = b.Name,
                Books = b.Books.Select(b => new BookModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorName = b.Author.Name,
                    GenreName = b.Genre.Name,
                    PublisherName = b.Publisher.Name,
                    Price = b.Price,
                    Reviews = b.Reviews.Select(w => new ReviewModel
                    {
                        Id = w.Id,
                        CustomerUsername = w.Customer.Username,
                        BookTitle = w.Book.Title,
                        Rating = w.Rating,
                        Comment = w.Comment

                    }).ToList(),
                    PurchaseHistories = b.PurchaseHistories.Select(w => new PurchaseHistoryModel
                    {
                        Id = w.Id,
                        BookTitle = w.Book.Title,
                        CustomerUsername = w.Customer.Username,
                        PurchaseDate = w.PurchaseDate

                    }).ToList(),
                    Wishlists = b.Wishlists.Select(w => new WishListModel
                    {
                        Id = w.Id,
                        CustomerName = w.Customer.Username
                    }).ToList()
                }).ToList()

            })
            .ToListAsync();

            if (authors == null || !authors.Any())
            {
                return BadRequest("No authors were found.");
            }

            return Ok(authors);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetAuthor(int id)
        {
            var author = await _dbContext.Authors.Include(b => b.Books).Where(b => b.Id == id).Select(b => new AuthorModel
            {
                Id = b.Id,
                Name = b.Name,
                Books = b.Books.Select(b => new BookModel
                {
                    Id = b.Id,
                    Title = b.Title,
                    AuthorName = b.Author.Name,
                    GenreName = b.Genre.Name,
                    PublisherName = b.Publisher.Name,
                    Price = b.Price,
                    Reviews = b.Reviews.Select(w => new ReviewModel
                    {
                        Id = w.Id,
                        CustomerUsername = w.Customer.Username,
                        BookTitle = w.Book.Title,
                        Rating = w.Rating,
                        Comment = w.Comment

                    }).ToList(),
                    PurchaseHistories = b.PurchaseHistories.Select(w => new PurchaseHistoryModel
                    {
                        Id = w.Id,
                        BookTitle = w.Book.Title,
                        CustomerUsername = w.Customer.Username,
                        PurchaseDate = w.PurchaseDate

                    }).ToList(),
                    Wishlists = b.Wishlists.Select(w => new WishListModel
                    {
                        Id = w.Id,
                        CustomerName = w.Customer.Username
                    }).ToList()
                }).ToList()

            })
            .ToListAsync();

            if (author == null)
            {
                return BadRequest($"No author with ID {id} was found.");
            }

            return Ok(author);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAuthor(AuthorModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = new Author
            {
                Name = model.Name
            };

            _dbContext.Authors.Add(author);
            await _dbContext.SaveChangesAsync();

            return Ok(author);
        }

        [HttpPut]
        [Route("{id}/update")]
        public async Task<IActionResult> UpdateAuthor(int id, AuthorModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var author = await _dbContext.Authors.FindAsync(id);

            if (author == null)
            {
                return BadRequest($"No author with ID {id} was found.");
            }

            author.Name = model.Name;

            _dbContext.Authors.Update(author);
            await _dbContext.SaveChangesAsync();

            return Ok(author);
        }

        [HttpDelete]
        [Route("{id}/delete")]
        public async Task<IActionResult> DeleteAuthor(int id)
        {
            var author = await _dbContext.Authors.FindAsync(id);

            if (author == null)
            {
                return BadRequest($"No author with ID {id} was found.");
            }

            _dbContext.Authors.Remove(author);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }
    }
}


