using BusinessLayer.DTOs.Book;
using BusinessLayer.Mapper;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class BookService : BaseService, IBookService
    {
        private readonly BookHubDBContext _dbContext;

        public BookService(BookHubDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public IQueryable<Book> GetAllBooksQuery()
        {
            return _dbContext.Books;
        }

        private async Task<List<BookDTO>> GetBooksCommonQuery(IQueryable<Book> query)
        {
            var books = await query
                .Include(b => b.AuthorBooks)
                .ThenInclude(b => b.Author)
                .Include(b => b.GenreBooks)
                .ThenInclude(gb => gb.Genre)
                .Include(b => b.Publisher)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.Customer)
                .ToListAsync();

            return books.Select(b => b.MapToBookDTO()).ToList();
        }

        public async Task<List<BookDTO>> GetBooksAsync()
        {
            return await GetBooksCommonQuery(GetAllBooksQuery());
        }

        public async Task<BookDTO?> GetBookAsync(int id)
        {
            var books = await GetBooksCommonQuery(GetAllBooksQuery().Where(b => b.Id == id));
            return books.FirstOrDefault();
        }

        public async Task<List<BookDTO>> SearchBooksAsync(string title, string description, decimal? price, string genre, string author)
        {
            var query = GetAllBooksQuery();

            if (!string.IsNullOrWhiteSpace(title))
            {
                query = query.Where(b => b.Title.Contains(title));
            }
            if (!string.IsNullOrWhiteSpace(description))
            {
                query = query.Where(b => b.Description.Contains(description));
            }
            if (price.HasValue)
            {
                query = query.Where(b => b.Price == price);
            }
            if (!string.IsNullOrWhiteSpace(genre))
            {
                query = query.Where(b => b.GenreBooks.Any(bg => bg.Genre.Name == genre));
            }
            if (!string.IsNullOrWhiteSpace(author))
            {
                query = query.Where(b => b.AuthorBooks.Any(ab => ab.Author.Name == author));
            }

            return await GetBooksCommonQuery(query);
        }

        public async Task<BookDTO> CreateBookAsync(BookDTO model)
        {
            var book = new Book
            {
                Title = model.Title,
                Price = model.Price,
                Description = model.Description,
            };

            var book2 = EntityMapper.MapToBook(model);

            _dbContext.Books.Add(book2);
            await SaveAsync(true);
            return book.MapToBookDTO();
        }

        public async Task<BookDTO> UpdateBookAsync(int id, BookUpdateDTO model)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
            {
                return null;
            }

            book.Title = model.Title;
            book.Price = model.Price;
            book.Description = model.Description;

            await _dbContext.SaveChangesAsync();
            return book.MapToBookDTO();
        }

        public async Task<bool> DeleteBookAsync(int id)
        {
            var book = await _dbContext.Books.FindAsync(id);
            if (book == null)
            {
                return false;
            }

            _dbContext.Books.Remove(book);
            await SaveAsync(true);
            return true;
        }
    }
}
