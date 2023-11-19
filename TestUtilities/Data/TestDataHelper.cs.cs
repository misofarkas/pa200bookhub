using DataAccessLayer.Models;
using System.Xml.Linq;

namespace TestUtilities.Data
{
    public static class TestDataHelper
    {
        public static List<Book> GetFakeBooks()
        {
            return new List<Book>()
            {
                new Book { 
                    Id = 1, 
                    Title = "1984", 
                    AuthorId = 1, 
                    GenreId = 1, 
                    PublisherId = 1, 
                    Price = 15.99M, 
                    Description = "Nineteen Eighty-Four (also published as 1984) is a dystopian novel and cautionary tale by English writer George Orwell. It was published on 8 June 1949" 
                },
                new Book { 
                    Id = 2, 
                    Title = "Harry Potter and the Philosopher's Stone", 
                    AuthorId = 2, 
                    GenreId = 2, 
                    PublisherId = 2, 
                    Price = 20.99M, 
                    Description = "Harry Potter and the Philosopher's Stone is a fantasy novel written by British author J. K. Rowling." },
            };
        }
    }
}
