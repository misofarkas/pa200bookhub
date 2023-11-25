using BusinessLayer.DTOs;
using BusinessLayer.DTOs.Author;
using BusinessLayer.Services.Author;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUtilities.Data;
using TestUtilities.MockedObjects;

namespace BusinessLayer.Tests.services
{
    public class AuthorServiceTests
    {
        private MockedDependencyInjectionBuilder _serviceProviderBuilder;

        public AuthorServiceTests()
        {
            _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
                .AddServices()
                .AddMockdDBContext();
        }


        [Fact]
        public async Task GetAll_WhenCalled_ReturnsAllAuthors()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);

                var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();

                // Act
                var result = await authorService.GetAll();

                // Assert
                Assert.NotNull(result);
                Assert.Equal(TestDataHelper.GetFakeAuthors().Count, result.Count);
            }
        }

        [Fact]
        public async Task CreateAuthor_ValidData_CreatesNewAuthor()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);

                var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();

                var newAuthorDTO = new AuthorCreateUpdateDTO { Name = "New Author" };

                // Act
                var result = await authorService.CreateAuthor(newAuthorDTO);

                // Assert
                Assert.True(result);
                var authorInDb = dbContext.Authors.FirstOrDefault(a => a.Name == newAuthorDTO.Name);
                Assert.NotNull(authorInDb);
                Assert.Equal(newAuthorDTO.Name, authorInDb.Name);
            }
        }


        [Fact]
        public async Task UpdateAuthor_ValidId_UpdatesAuthor()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);

                var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();

                var authorIdToUpdate = TestDataHelper.GetFakeAuthors().First().Id;
                var updatedAuthorDTO = new AuthorCreateUpdateDTO { Name = "Updated Author" };

                // Act
                var result = await authorService.UpdateAuthor(authorIdToUpdate, updatedAuthorDTO);

                // Assert
                Assert.True(result);
                var author = dbContext.Authors.Find(authorIdToUpdate);
                Assert.NotNull(author);
                Assert.Equal("Updated Author", author.Name);
            }
        }


        [Fact]
        public async Task DeleteAuthor_ValidId_DeletesAuthor()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);

                var authorService = scope.ServiceProvider.GetRequiredService<IAuthorService>();

                var authorIdToDelete = TestDataHelper.GetFakeAuthors().First().Id;

                // Act
                var result = await authorService.DeleteAuthor(authorIdToDelete);

                // Assert
                Assert.True(result);
                var author = dbContext.Authors.Find(authorIdToDelete);

            }
        }
    }
}
