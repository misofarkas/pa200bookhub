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
                    PublisherId = 1, 
                    Price = 15.99M, 
                    Description = "Nineteen Eighty-Four (also published as 1984) is a dystopian novel and cautionary tale by English writer George Orwell. It was published on 8 June 1949" 
                },
                new Book { 
                    Id = 2, 
                    Title = "Harry Potter and the Philosopher's Stone", 
                    PublisherId = 2, 
                    Price = 20.99M, 
                    Description = "Harry Potter and the Philosopher's Stone is a fantasy novel written by British author J. K. Rowling." },
            };
        }

        public static List<Customer> GetFakeCustomers()
        {
            return new List<Customer>()
            {
                new Customer
                {
                    Id = 1,
                    Username = "Test",
                    isDeleted = false,
                    Reviews = new List<Review>(),
                    Wishlists = new List<Wishlist>(),
                    PurchaseHistories = new List<PurchaseHistory>(),
                },
                new Customer
                {
                    Id = 2,
                    Username = "Test2",
                    isDeleted = false,
                    Reviews = new List<Review>(),
                    Wishlists = new List<Wishlist>(),
                    PurchaseHistories = new List<PurchaseHistory>(),
                }
            };
        }
        public static List<Author> GetFakeAuthors()
        {
            return new List<Author>()
            {
                new Author { Id = 1, Name = "Author One" },
                new Author { Id = 2, Name = "Author Two" }
            };
        }
    }
}
