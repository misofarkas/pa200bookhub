using BusinessLayer.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IGenreService : IBaseService
    {
        Task<List<GenreDTO>> GetAllGenresAsync();
        Task<GenreDTO> GetGenreAsync(int id);
        Task<GenreDTO> UpdateGenreAsync(int id, GenreDTO genre);
    }
}
