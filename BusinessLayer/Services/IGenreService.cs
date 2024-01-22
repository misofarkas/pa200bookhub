using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs.Genre;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IGenreService : IBaseService
    {
        Task<List<GenreDTO>> GetAllGenresAsync();
        Task<GenreDTO> GetGenreAsync(int id);
        Task<GenreCreateUpdateDTO> UpdateGenreAsync(int id, GenreCreateUpdateDTO genre);
        Task<GenreCreateUpdateDTO> CreateGenreAsync(GenreCreateUpdateDTO genre);

        Task<IEnumerable<GenreDTO>> SearchGenres(string genreName);

        Task<bool> DeleteGenreAsync(int id);
    }
}
