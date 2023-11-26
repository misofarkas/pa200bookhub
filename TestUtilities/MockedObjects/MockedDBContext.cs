using DataAccessLayer.Data;
using EntityFrameworkCore.Testing.NSubstitute.Helpers;
using Microsoft.EntityFrameworkCore;
using TestUtilities.Data;

namespace TestUtilities.MockedObjects
{
    public static class MockedDBContext
    {
        public static string RandomDBName => Guid.NewGuid().ToString();

        public static DbContextOptions<BookHubDBContext> GenerateNewInMemoryDBContextOptons()
        {
            var dbContextOptions = new DbContextOptionsBuilder<BookHubDBContext>()
                .UseInMemoryDatabase(RandomDBName)
                .Options;

            return dbContextOptions;
        }

        public static BookHubDBContext CreateFromOptions(DbContextOptions<BookHubDBContext> options)
        {
            var dbContextToMock = new BookHubDBContext(options);

            var dbContext = new MockedDbContextBuilder<BookHubDBContext>()
                .UseDbContext(dbContextToMock)
                .UseConstructorWithParameters(options)
                .MockedDbContext;

            PrepareData(dbContext);

            return dbContext;
        }

        public static void PrepareData(BookHubDBContext dbContext)
        {
            dbContext.Books.AddRange(TestDataHelper.GetFakeBooks());
            dbContext.Customers.AddRange(TestDataHelper.GetFakeCustomers());
            dbContext.Authors.AddRange(TestDataHelper.GetFakeAuthors());
            dbContext.Publishers.AddRange(TestDataHelper.GetFakePublishers());
            dbContext.AuthorBooks.AddRange(TestDataHelper.GetFakeAuthorBooks());
            dbContext.GenreBooks.AddRange(TestDataHelper.GetFakeGenreBooks());

            dbContext.SaveChanges();
        }

        public static async Task PrepareDataAsync(BookHubDBContext dbContext)
        {
            await dbContext.Books.AddRangeAsync(TestDataHelper.GetFakeBooks());
            await dbContext.Customers.AddRangeAsync(TestDataHelper.GetFakeCustomers());
            await dbContext.Authors.AddRangeAsync(TestDataHelper.GetFakeAuthors());
            await dbContext.Publishers.AddRangeAsync(TestDataHelper.GetFakePublishers());
            await dbContext.AuthorBooks.AddRangeAsync(TestDataHelper.GetFakeAuthorBooks());
            await dbContext.GenreBooks.AddRangeAsync(TestDataHelper.GetFakeGenreBooks());

            await dbContext.SaveChangesAsync();
        }
    }
}
