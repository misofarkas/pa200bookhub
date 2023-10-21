using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace DataAccessLayer.Data
{
    public static class DataInitializer
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            var authors = PrepareAuthors();

            var publishers = PreparePublishers();
            var books = PrepareBooks();

            modelBuilder.Entity<Author>().HasData(authors);
            modelBuilder.Entity<Publisher>().HasData(publishers);
            modelBuilder.Entity<Book>().HasData(books);
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
                new Book { Id = 1, Title = "1984", AuthorId = 1, GenreId = 1, PublisherId = 1, Price = 15.99M },
                new Book { Id = 2, Title = "Harry Potter and the Philosopher's Stone", AuthorId = 2, GenreId = 2, PublisherId = 2, Price = 20.99M },
                new Book { Id = 3, Title = "The Hobbit", AuthorId = 3, GenreId = 3, PublisherId = 3, Price = 25.99M }
            };
        }
    }
}
