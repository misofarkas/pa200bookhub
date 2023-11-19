using BusinessLayer.DTOs;
using DataAccessLayer.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface IBookService : IBaseService
    {
        IQueryable<Book> GetAllBooksQuery();

        Task<List<BookDTO>> GetBooksAsync();

        Task<BookDTO?> GetBookAsync(int id);

        Task<List<BookDTO>> SearchBooksAsync(string title, string description, decimal? price, string genre, string author);

        //Task<BookDTO> CreateBookAsync(BookCreateDTO model);

        //Task<BookDTO> UpdateBookAsync(int id, BookUpdateDTO model);

        Task<bool> DeleteBookAsync(int id);
    }
}
