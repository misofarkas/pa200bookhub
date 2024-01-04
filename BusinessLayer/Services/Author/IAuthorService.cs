using BusinessLayer.DTOs.Author;

namespace BusinessLayer.Services
{
    public interface IAuthorService : IBaseService
    {
        Task<List<AuthorWithoutBooksDTO>> GetAll();

        Task<AuthorDTO?> GetByAuthorId(int authorId);

        Task<AuthorDTO?> GetByAuthorName(string authorName);

        Task<IEnumerable<AuthorDTO>> SearchAuthor(string authorName);

        Task<AuthorCreateUpdateDTO> CreateAuthor(AuthorCreateUpdateDTO authorDTO);

        Task<AuthorCreateUpdateDTO> UpdateAuthor(int id, AuthorCreateUpdateDTO authorDTO);

        Task<bool> DeleteAuthor(int authorId);

    }
}
