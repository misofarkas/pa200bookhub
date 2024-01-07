using Azure;
using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.Enums;
using BusinessLayer.Mapper;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BusinessLayer.Services
{
    public class AuthorService : BaseService , IAuthorService
    {
        private readonly BookHubDBContext _dbContext;
        public AuthorService(BookHubDBContext dBContext) : base(dBContext)
        {
            _dbContext = dBContext;
        }

        public async Task<AuthorCreateUpdateDTO> CreateAuthor(AuthorCreateUpdateDTO authorDTO)
        {
            var author = EntityMapper.MapToAuthor(authorDTO);
            if (author == null) {
                throw new Exception("There was an error creating Author");
            }
            _dbContext.Authors.Add(author);
            await SaveAsync(true);
            return authorDTO;
        }

        public async Task<bool> DeleteAuthor(int authorId)
        {
            var author = await _dbContext.Authors.FindAsync(authorId);
            if (author == null)
            {
                throw new Exception($"No author with ID {authorId} has been found");
            }
            var isAuthorOfAnyBook = await _dbContext.AuthorBooks.AnyAsync(ab => ab.AuthorId == authorId);

            if (isAuthorOfAnyBook)
            {
                throw new Exception("Author is associated with a book");
            }

            _dbContext.Authors.Remove(author);
            await SaveAsync(true);
            return true;
        }

        public async Task<List<AuthorWithoutBooksDTO>> GetAll()
        {
            var authors = await _dbContext.Authors.ToListAsync();
            return authors.Select(a => DTOMapper.MapToAuthorDTOList(a)).ToList();
        }

        public async Task<AuthorDTO?> GetByAuthorId(int authorId)
        {
            var author = await _dbContext.Authors
                .Include(b => b.AuthorBooks)
                .ThenInclude(ab => ab.Book)
                .ThenInclude(book => book.GenreBooks)
                .ThenInclude(gb => gb.Genre)
                .Include(b => b.AuthorBooks)
                .ThenInclude(ab => ab.Book)
                .ThenInclude(b => b.Publisher)
                .FirstOrDefaultAsync(a => a.Id == authorId);
            if (author == null)
            {
                return null;
            }
            return DTOMapper.MapToAuthorDTO(author);
        }

        public async Task<AuthorDTO?> GetByAuthorName(string authorName)
        {
            var author = await _dbContext.Authors
                .Include(b => b.AuthorBooks)
                .ThenInclude(ab => ab.Book)
                .ThenInclude(book => book.GenreBooks)
                .ThenInclude(gb => gb.Genre)
                .Include(b => b.AuthorBooks)
                .ThenInclude(ab => ab.Book)
                .ThenInclude(b => b.Publisher)
                .FirstOrDefaultAsync(a => a.Name == authorName);
            if (author == null)
            {
                return null;
            }
            return DTOMapper.MapToAuthorDTO(author);
        }

        public async Task<IEnumerable<AuthorDTO>> SearchAuthor(string authorName)
        {
            IQueryable<Author> query = _dbContext.Authors
                .Include(b => b.AuthorBooks)
                .ThenInclude(ab => ab.Book)
                .ThenInclude(book => book.GenreBooks)
                .ThenInclude(gb => gb.Genre)
                .Include(b => b.AuthorBooks)
                .ThenInclude(ab => ab.Book)
                .ThenInclude(b => b.Publisher);


            query = query.Where(b => b.Name.Contains(authorName));

            var authors = await query
                .ToListAsync();

            return authors.Select(a => DTOMapper.MapToAuthorDTO(a));
        }

        public async Task<AuthorCreateUpdateDTO> UpdateAuthor(int id, AuthorCreateUpdateDTO authorDTO)
        {
            var author = await _dbContext.Authors.FindAsync(id);
            if (author == null)
            {
                throw new Exception("There was an error updating Author");
            }

            author.Name = authorDTO.Name;
            _dbContext.Authors.Update(author);
            await SaveAsync(true);
            return authorDTO;
        }
    }
}
