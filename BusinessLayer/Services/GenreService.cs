using BusinessLayer.DTOs.Genre;
using BusinessLayer.Mapper;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Mapster;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class GenreService : BaseService, IGenreService
    {
        private readonly BookHubDBContext _dbContext;

        public GenreService(BookHubDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<GenreDTO>> GetAllGenresAsync()
        {
            var genres = await _dbContext.Genre.ToListAsync();
            return genres.Select(DTOMapper.MapToGenreDTO).ToList();
        }

        public async Task<GenreDTO> GetGenreAsync(int id)
        {
            var genre = await _dbContext.Genre.FindAsync(id);
            if (genre == null)
            {
                return null;
            }
            return DTOMapper.MapToGenreDTO(genre);
        }

        public async Task<GenreCreateUpdateDTO> UpdateGenreAsync(int id, GenreCreateUpdateDTO genreDTO)
        {
            var genre = await _dbContext.Genre.FindAsync(id);
            if (genre != null)
            {
                genre.Name = genreDTO.Name;
                await SaveAsync(true);
                return DTOMapper.MapToGenreCreateUpdateDTO(genre);
            }
            return null;
        }

        public async Task<IEnumerable<GenreDTO>> SearchGenres(string genreName)
        {
            IQueryable<Genre> query = _dbContext.Genre
                .Include(b => b.GenreBooks)
                .ThenInclude(ab => ab.Book);

            var searchTerm = genreName?.ToLower();
            query = query.Where(b => b.Name.Contains(searchTerm));

            var genres = await query
                .ToListAsync();

            return genres.Select(a => DTOMapper.MapToGenreDTO(a));
        }

        public async Task<GenreCreateUpdateDTO> CreateGenreAsync(GenreCreateUpdateDTO genreDTO)
        {
            var genre = EntityMapper.MapToGenre(genreDTO);
            if (genre == null)
            {
                throw new Exception("There was an error creating Genre");
            }
            _dbContext.Genre.Add(genre);
            await SaveAsync(true);
            return genreDTO;
        }

        public async Task<bool> DeleteGenreAsync(int id)
        {
            var genre = await _dbContext.Genre.FindAsync(id);
            if (genre == null)
            {
                throw new Exception("No genre with this id exists ");
            }
            var isPartOfBook = await _dbContext.GenreBooks.AnyAsync(gb => gb.GenreId == id);
            if (isPartOfBook)
            {
                throw new Exception("Genre is part of a book");
            }
            _dbContext.Genre.Remove(genre);
            await SaveAsync(true);
            return true;
        }
    }
}
