using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.Enums;
using BusinessLayer.Mapper;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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

        public async Task<PaginatedResult<BookDTO>> SearchBooksWithCriteria(BookSearchCriteriaDTO searchCriteria, int page, int pageSize)
        {
            IQueryable<Book> query = _dbContext.Books
                .Include(b => b.AuthorBooks)
                .ThenInclude(b => b.Author)
                .Include(b => b.GenreBooks)
                .ThenInclude(gb => gb.Genre)
                .Include(b => b.Publisher)
                .Include(b => b.Reviews)
                .ThenInclude(r => r.Customer);

            switch (searchCriteria.SearchIn)
            {
                case BookSearchField.Title:
                    query = query.Where(p => p.Title.Contains(searchCriteria.Query));
                    break;
                case BookSearchField.Desciption:
                    query = query.Where(p => p.Description.Contains(searchCriteria.Query));
                    break;
                case BookSearchField.Author:
                    query = query.Where(b => b.AuthorBooks.Any(ab => ab.Author.Name.Contains(searchCriteria.Query)));
                    break;
                case BookSearchField.Genre:
                    query = query.Where(b => b.GenreBooks.Any(ab => ab.Genre.Name.Contains(searchCriteria.Query)));
                    break;
                case BookSearchField.Publisher:
                    query = query.Where(b => b.Publisher.Name.Contains(searchCriteria.Query));
                    break;
            }

            var totalCount = await query.CountAsync();
            var books = await query
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return new PaginatedResult<BookDTO>
            {
                Items = books.Select(b => b.MapToBookDTO()).ToList(),
                TotalCount = totalCount
            };
        }

        public async Task<BookDTO> CreateBookAsync(BookCreateUpdateDTO model)
        {
            var publisher = await _dbContext.Publishers.FindAsync(model.PublisherId);
            if (publisher == null)
            {
                throw new Exception("Publisher not found");
            }
            var authors = await _dbContext.Authors.Where(a => model.AuthorIds.Contains(a.Id)).ToListAsync();
            if (authors.Count != model.AuthorIds.Count)
            {
                throw new Exception("Author not found");
            }
            var genres = await _dbContext.Genre.Where(g => model.GenreIds.Contains(g.Id)).ToListAsync();
            if (genres.Count != model.GenreIds.Count)
            {
                throw new Exception("Genre not found");
            }
            var newBook = EntityMapper.MapToBook(model);
            newBook.Publisher = publisher;
            newBook.AuthorBooks = authors.Select(a => new AuthorBook { Author = a, Book = newBook }).ToList();
            newBook.GenreBooks = genres.Select(g => new GenreBook { Genre = g, Book = newBook }).ToList();

            _dbContext.Books.Add(newBook);
            await SaveAsync(true);
            return newBook.MapToBookDTO();
        }

        public async Task<BookDTO> UpdateBookAsync(int id, BookCreateUpdateDTO model)
        {
            var book = await _dbContext.Books
                .Include(b => b.AuthorBooks)
                .Include(b => b.GenreBooks)
                .Include(b => b.Publisher)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                throw new Exception("Book Not found");
            }

            book.Title = model.Title;
            book.Price = model.Price;
            book.Description = model.Description;
            book.PublisherId = model.PublisherId;
            if (model.AuthorIds is not null)
            {
                if (model.AuthorIds.Count == 0)
                {
                    throw new Exception("AuthorIds cannot be empty");
                }

                var authors = await _dbContext.Authors.Include(b => b.AuthorBooks).Where(a => model.AuthorIds.Contains(a.Id)).ToListAsync();
                if (authors.Count != model.AuthorIds.Count)
                {
                    throw new Exception("Author not found");
                }

                var authorsToRemove = book.AuthorBooks.Where(ab => !authors.Any(a => a.Id == ab.AuthorId)).ToList();
                var newAuthors = authors.Where(a => !book.AuthorBooks.Any(ab => ab.AuthorId == a.Id)).ToList();
                foreach (var author in authorsToRemove)
                {
                    book.AuthorBooks.Remove(author);
                }
                foreach (var author in newAuthors)
                {
                    book.AuthorBooks.Add(new AuthorBook { Author = author, Book = book });
                }

            }
            if (model.GenreIds is not null)
            {
                if (model.GenreIds.Count == 0)
                {
                    throw new Exception("GenreIds cannot be empty");
                }

                var genres = await _dbContext.Genre.Include(b => b.GenreBooks).Where(g => model.GenreIds.Contains(g.Id)).ToListAsync();
                if (genres.Count != model.GenreIds.Count)
                {
                    throw new Exception("Genre not found");
                }

                var genresToRemove = book.GenreBooks.Where(gb => !genres.Any(g => g.Id == gb.GenreId)).ToList();
                var newGenres = genres.Where(g => !book.GenreBooks.Any(gb => gb.GenreId == g.Id)).ToList();
                foreach (var genre in genresToRemove)
                {
                    book.GenreBooks.Remove(genre);
                }
                foreach (var genre in newGenres)
                {
                    book.GenreBooks.Add(new GenreBook { Genre = genre, Book = book });
                }


            }

            _dbContext.Entry(book).State = EntityState.Modified;
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
