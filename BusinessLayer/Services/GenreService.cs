using BusinessLayer.DTOs;
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
            return DTOMapper.MapToGenreDTO(genre);
        }

        public async Task<GenreDTO> UpdateGenreAsync(int id, GenreDTO genreDTO)
        {
            var genre = await _dbContext.Genre.FindAsync(id);
            if (genre != null)
            {
                genre.Name = genreDTO.Name;
                await SaveAsync(true);
                return DTOMapper.MapToGenreDTO(genre);
            }
            return null;
        }

        public async Task<IEnumerable<GenreDTO>> SearchGenres(string genreName)
        {
            IQueryable<Genre> query = _dbContext.Genre
                .Include(b => b.GenreBooks)
                .ThenInclude(ab => ab.Book);


            query = query.Where(b => b.Name.Contains(genreName));

            var genres = await query
                .ToListAsync();

            return genres.Select(a => DTOMapper.MapToGenreDTO(a));
        }
    }
}
