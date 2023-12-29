using BusinessLayer.DTOs;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
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
            var genres = await _dbContext.Genres.ToListAsync();
            return genres.Select(g => new GenreDTO { Id = g.Id, Name = g.Name }).ToList();
        }

        public async Task<GenreDTO> GetGenreAsync(int id)
        {
            var genre = await _dbContext.Genres.FindAsync(id);
            if (genre != null)
            {
                return new GenreDTO { Id = genre.Id, Name = genre.Name };
            }
            return null;
        }

        public async Task<GenreDTO> UpdateGenreAsync(int id, GenreDTO genreDTO)
        {
            var genre = await _dbContext.Genres.FindAsync(id);
            if (genre != null)
            {
                genre.Name = genreDTO.Name;
                await SaveAsync(true);
                return new GenreDTO { Id = genre.Id, Name = genre.Name };
            }
            return null;
        }
    }
}
