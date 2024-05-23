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
            var authorbooks = PrepareAuthorBooks();
            var genrebooks = PrepareGenreBooks();

            modelBuilder.Entity<Author>().HasData(authors);
            modelBuilder.Entity<Genre>().HasData(genres);
            modelBuilder.Entity<Publisher>().HasData(publishers);
            modelBuilder.Entity<Book>().HasData(books);
            modelBuilder.Entity<Customer>().HasData(customers);
            modelBuilder.Entity<PurchaseHistory>().HasData(purchaseHistory);
            modelBuilder.Entity<Review>().HasData(review);
            modelBuilder.Entity<Wishlist>().HasData(wishlist);
            modelBuilder.Entity<AuthorBook>().HasData(authorbooks);
            modelBuilder.Entity<GenreBook>().HasData(genrebooks);
        }

        private static List<Author> PrepareAuthors()
        {
            return new List<Author>()
            {
                new Author { Id = 1, Name = "George Orwell" },
                new Author { Id = 2, Name = "J.K. Rowling" },
                new Author { Id = 3, Name = "J.R.R. Tolkien" },
                new Author { Id = 4, Name = "Agatha Christie" },
                new Author { Id = 5, Name = "Stephen King" }
            };
        }

        private static List<Genre> PrepareGenres()
        {
            return new List<Genre>()
            {
                new Genre { Id = 1, Name = "Fiction" },
                new Genre { Id = 2, Name = "Fantasy" },
                new Genre { Id = 3, Name = "Adventure" },
                new Genre { Id = 4, Name = "Mystery" },
                new Genre { Id = 5, Name = "Horror" }

            };
        }


        private static List<Publisher> PreparePublishers()
        {
            return new List<Publisher>()
            {
                new Publisher { Id = 1, Name = "Penguin Books" },
                new Publisher { Id = 2, Name = "Bloomsbury" },
                new Publisher { Id = 3, Name = "HarperCollins" },
                new Publisher { Id = 4, Name = "Simon & Schuster" },
                new Publisher { Id = 5, Name = "Scholastic" }
            };
        }

        private static List<Book> PrepareBooks()
        {
            return new List<Book>()
            {
                new Book { Id = 1, Title = "1984", PrimaryGenreId = 1, PublisherId = 1, Price = 15.99M, Description = "Nineteen Eighty-Four (also published as 1984) is a dystopian novel and cautionary tale by English writer George Orwell. It was published on 8 June 1949" },
                new Book { Id = 2, Title = "Harry Potter and the Philosopher's Stone", PrimaryGenreId = 2, PublisherId = 2, Price = 20.99M, Description = "Harry Potter and the Philosopher's Stone is a fantasy novel written by British author J. K. Rowling." },
                new Book { Id = 3, Title = "The Hobbit", PrimaryGenreId = 3, PublisherId = 3, Price = 25.99M, Description = "The Hobbit, or There and Back Again is a children's fantasy novel by English author J. R. R. Tolkien." },
                new Book { Id = 4, Title = "Murder on the Orient Express", PrimaryGenreId = 4, PublisherId = 4, Price = 18.99M, Description = "Murder on the Orient Express is a detective novel by English writer Agatha Christie." },
                new Book { Id = 5, Title = "The Shining", PrimaryGenreId = 5, PublisherId = 5, Price = 22.99M, Description = "The Shining is a horror novel by American author Stephen King." }

            };
        }

        private static List<AuthorBook> PrepareAuthorBooks()
        {
            return new List<AuthorBook>()
            {
                new AuthorBook {AuthorId = 1, BookId = 1},
                new AuthorBook { AuthorId = 2, BookId = 2},
                new AuthorBook { AuthorId = 3, BookId = 3},
                new AuthorBook { AuthorId = 4, BookId = 4},
                new AuthorBook { AuthorId = 5, BookId = 5},
            };
        }
        private static List<GenreBook> PrepareGenreBooks()
        {
            return new List<GenreBook>()
            {
                new GenreBook { GenreId = 1, BookId = 1},
                new GenreBook { GenreId = 2, BookId = 2},
                new GenreBook { GenreId = 3, BookId = 3},
                new GenreBook { GenreId = 4, BookId = 4},
                new GenreBook { GenreId = 5, BookId = 5},
            };
        }

        private static List<Customer> PrepareCustomers()
        {
            return new List<Customer>()
            {
                new Customer { Id = 1, Username = "Janko"},
                new Customer { Id = 2, Username = "AnnaB" },
                new Customer { Id = 3, Username = "MikeW" }
            };
        }

        private static List<PurchaseHistory> PreparePurchaseHistory()
        {
            return new List<PurchaseHistory> 
            { 
                new PurchaseHistory { Id = 1, BookId = 1, CustomerId = 1, PurchaseDate = new DateTimeOffset(2023, 10, 10, 0, 0, 0, TimeSpan.Zero), TotalPrice = 15.99M, Paid = false },
                new PurchaseHistory { Id = 2, BookId = 3, CustomerId = 1, PurchaseDate = new DateTimeOffset(2023, 10, 10, 0, 0, 0, TimeSpan.Zero), TotalPrice = 25.99M, Paid = true },
                new PurchaseHistory { Id = 3, BookId = 4, CustomerId = 2, PurchaseDate = new DateTimeOffset(2023, 10, 10, 0, 0, 0, TimeSpan.Zero), TotalPrice = 18.99M, Paid = true },
                new PurchaseHistory { Id = 4, BookId = 5, CustomerId = 3, PurchaseDate = new DateTimeOffset(2023, 10, 10, 0, 0, 0, TimeSpan.Zero), TotalPrice = 22.99M, Paid = true }

            };
        }

        private static List<Review> PrepareReviews() 
        {
            return new List<Review>()
            {
                new Review { Id = 1, BookId = 1, CustomerId = 1, Rating = 5, Comment = "Very nice book!"},
                new Review { Id = 2, BookId = 3, CustomerId = 1, Rating = 1, Comment = "Worst book!"},
                new Review { Id = 3, BookId = 4, CustomerId = 2, Rating = 4, Comment = "Intriguing and thrilling!" },
                new Review { Id = 4, BookId = 5, CustomerId = 3, Rating = 3, Comment = "Scary but a bit too long." }
            };
        
        }

        private static List<Wishlist> PrepareWishlist()
        {
            return new List<Wishlist>()
            {
                new Wishlist { Id = 1, BookId = 2, CustomerId = 1},
                new Wishlist { Id = 2, BookId = 4, CustomerId = 2 },
                new Wishlist { Id = 3, BookId = 5, CustomerId = 3 }
            };
        }

    }
}
