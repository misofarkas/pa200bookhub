using BusinessLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TestUtilities.MockedObjects;
using TestUtilities.Data;
using DataAccessLayer.Data;
using BusinessLayer.DTOs.Book;
using BusinessLayer.DTOs.Author;
using BusinessLayer.DTOs;

namespace BusinessLayer.Tests.services
{
    public class BookServiceTests
    {
        private MockedDependencyInjectionBuilder _serviceProviderBuilder;

        public BookServiceTests()
        {
            _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
                .AddServices()
                .AddMockdDBContext();
        }

        [Fact]
        public async Task GetBooksAsync_WhenCalled_ReturnsAllBooks()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);

                var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

                // Act
                var result = await bookService.GetBooksAsync();

                // Assert
                Assert.NotNull(result);
                Assert.Equal(TestDataHelper.GetFakeBooks().Count, result.Count);
            }
        }

        [Fact]
        public async Task GetBookAsync_ById_ReturnsBook()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);

                var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

                var bookToGet = TestDataHelper.GetFakeBooks().First();
                // Act
                var result = await bookService.GetBookAsync(bookToGet.Id);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(bookToGet.Title, result.Title);
            }
        }

        [Fact]
        public async Task GetBookAsync_ByNonValid_ReturnsNull()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);

                var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

                var bookId = 999;
                // Act
                var result = await bookService.GetBookAsync(bookId);

                // Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task CreateBookAsync_Valid_CreatesNewBook()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);

                var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

                var newBookDTO = new BookCreateUpdateDTO
                {
                    Title = "Test",
                    Description = "TestDesc",
                    Price = 10,
                    PublisherId = 1,
                    PrimaryGenreId = 1,
                    AuthorIds = new List<int> { },
                    GenreIds = new List<int> { 1 }
                };
                // Act
                var result = await bookService.CreateBookAsync(newBookDTO);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(newBookDTO.Title, result.Title);

                var bookInDb = dbContext.Books.FirstOrDefault(c => c.Title == newBookDTO.Title);
                Assert.NotNull(bookInDb);
                Assert.Equal(newBookDTO.Title, bookInDb.Title);
            }
        }

        [Fact]
        public async Task UpdateBookAsync_Valid_UpdatesBook()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);

                var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

                var bookIdToUpdate = TestDataHelper.GetFakeBooks().First().Id;
                var updatedBookDTO = new BookCreateUpdateDTO { Title = "UpdatedTitle", PrimaryGenreId = 1, PublisherId = 1 };
                // Act
                var result = await bookService.UpdateBookAsync(bookIdToUpdate, updatedBookDTO);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("UpdatedTitle", result.Title);
            }
        }

        [Fact]
        public async Task DeleteBookAsync_ValidId_DeletesBook()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);
                var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

                var bookIdToDelete = 1;

                // Act
                var result = await bookService.DeleteBookAsync(bookIdToDelete);

                // Assert
                Assert.True(result);
                var customer = await dbContext.Books.FindAsync(bookIdToDelete);
                Assert.Null(customer);
            }
        }

        [Fact]
        public async Task DeleteBookAsync_NonValidId_ReturnsFalse()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);
                var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

                var bookIdToDelete = 999;

                // Act
                var result = await bookService.DeleteBookAsync(bookIdToDelete);

                // Assert
                Assert.False(result);
            }
        }
    }
}
