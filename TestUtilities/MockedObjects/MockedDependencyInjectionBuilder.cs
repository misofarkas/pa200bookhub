using BusinessLayer.Services;
using BusinessLayer.Services.Author;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TestUtilities.MockedObjects
{
    public class MockedDependencyInjectionBuilder
    {
        protected IServiceCollection _serviceCollection = new ServiceCollection();

        public MockedDependencyInjectionBuilder()
        {
        }

        public MockedDependencyInjectionBuilder AddMockdDBContext()
        {
            _serviceCollection = _serviceCollection
                .AddDbContext<BookHubDBContext>(options => options
                    .UseInMemoryDatabase(MockedDBContext.RandomDBName));

            return this;
        }

        public MockedDependencyInjectionBuilder AddScoped<T>(T objectToRegister)
            where T : class
        {
            _serviceCollection = _serviceCollection
                .AddScoped<T>(_ => objectToRegister);

            return this;
        }

        public ServiceProvider Create()
        {
            return _serviceCollection.BuildServiceProvider();
        }

        public MockedDependencyInjectionBuilder AddServices()
        {
            _serviceCollection = _serviceCollection
                .AddScoped<IBookService, BookService>();
            _serviceCollection = _serviceCollection
                .AddScoped<ICustomerService, CustomerService>();
            _serviceCollection = _serviceCollection
                .AddScoped<IAuthorService, AuthorService>();

            return this;
        }
    }
}
