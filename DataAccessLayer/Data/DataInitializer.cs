using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Data
{
    public static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var authors = PrepareAuthors();
            var genres = PrepareGenres();
            var publishers = PreparePublishers();
            var books = PrepareBooks();
            var customers = PrepareCustomers();
            var purchaseHistory = PreparePurchaseHistory();
            var review = PrepareReviews();
            var wishlist = PrepareWishlist();

            modelBuilder.Entity<Author>().HasData(authors);
            modelBuilder.Entity<Genre>().HasData(genres);
            modelBuilder.Entity<Publisher>().HasData(publishers);
            modelBuilder.Entity<Book>().HasData(books);
            modelBuilder.Entity<Customer>().HasData(customers);
            modelBuilder.Entity<PurchaseHistory>().HasData(purchaseHistory);
            modelBuilder.Entity<Review>().HasData(review);
            modelBuilder.Entity<Wishlist>().HasData(wishlist);
        }

        private static List<Author> PrepareAuthors()
        {
            return new List<Author>()
            {
                new Author { Id = 1, Name = "George Orwell" },
                new Author { Id = 2, Name = "J.K. Rowling" },
                new Author { Id = 3, Name = "J.R.R. Tolkien" }
            };
        }

        private static List<Genre> PrepareGenres()
        {
            return new List<Genre>()
            {
                new Genre { Id = 1, Name = "Fiction" },
                new Genre { Id = 2, Name = "Fantasy" },
                new Genre { Id = 3, Name = "Adventure" }
            };
        }


        private static List<Publisher> PreparePublishers()
        {
            return new List<Publisher>()
            {
                new Publisher { Id = 1, Name = "Penguin Books" },
                new Publisher { Id = 2, Name = "Bloomsbury" },
                new Publisher { Id = 3, Name = "HarperCollins" }
            };
        }

        private static List<Book> PrepareBooks()
        {
            return new List<Book>()
            {
                new Book { Id = 1, Title = "1984", AuthorId = 1, GenreId = 1, PublisherId = 1, Price = 15.99M, Description = "Nineteen Eighty-Four (also published as 1984) is a dystopian novel and cautionary tale by English writer George Orwell. It was published on 8 June 1949" },
                new Book { Id = 2, Title = "Harry Potter and the Philosopher's Stone", AuthorId = 2, GenreId = 2, PublisherId = 2, Price = 20.99M, Description = "Harry Potter and the Philosopher's Stone is a fantasy novel written by British author J. K. Rowling." },
                new Book { Id = 3, Title = "The Hobbit", AuthorId = 3, GenreId = 3, PublisherId = 3, Price = 25.99M, Description = "The Hobbit, or There and Back Again is a children's fantasy novel by English author J. R. R. Tolkien." }
            };
        }

        private static List<Customer> PrepareCustomers()
        {
            return new List<Customer>()
            {
                new Customer { Id = 1, Username = "Janko", Password = "password123" }
            };
        }

        private static List<PurchaseHistory> PreparePurchaseHistory()
        {
            return new List<PurchaseHistory> 
            { 
                new PurchaseHistory { Id = 1, BookId = 1, CustomerId = 1, PurchaseDate = new DateTime(2023, 10, 10) },
                new PurchaseHistory { Id = 2, BookId = 3, CustomerId = 1, PurchaseDate = new DateTime(2023, 8, 15) }
            };
        }

        private static List<Review> PrepareReviews() 
        {
            return new List<Review>()
            {
                new Review { Id = 1, BookId = 1, CustomerId = 1, Rating = 5, Comment = "Very nice book!"},
                new Review { Id = 2, BookId = 3, CustomerId = 1, Rating = 1, Comment = "Worst book!"},
            };
        
        }

        private static List<Wishlist> PrepareWishlist()
        {
            return new List<Wishlist>()
            {
                new Wishlist { Id = 1, BookId = 2, CustomerId = 1}
            };
        }

    }
}
