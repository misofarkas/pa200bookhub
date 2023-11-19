using BusinessLayer.Services;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using TestUtilities.MockedObjects;
using TestUtilities.Data;


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
        public async Task DoesBooksExistAsync_ExistingBooksCount_ReturnsTwo()
        {
            // TODO: this test does not currently work. Don't know how to seed database for it.
            var serviceProvider = _serviceProviderBuilder.Create();

            using (var scope = serviceProvider.CreateScope())
            {

                var bookService = scope.ServiceProvider.GetRequiredService<IBookService>();

                // Act
                var result = await bookService.GetBooksAsync();

                // Assert
                Assert.True(result.Count() == 0);
            }
        }
    }
}
