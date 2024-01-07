using BusinessLayer.DTOs.Author;

namespace BusinessLayer.Services.Author
{
    public interface IAuthorService : IBaseService
    {
        Task<List<AuthorWithoutBooksDTO>> GetAll();

        Task<AuthorDTO?> GetByAuthorId(int authorId);

        Task<AuthorDTO?> GetByAuthorName(string authorName);

        Task<bool> CreateAuthor(AuthorCreateUpdateDTO authorDTO);

        Task<bool> UpdateAuthor(int id, AuthorCreateUpdateDTO authorDTO);

        Task<bool> DeleteAuthor(int authorId);

    }
}
