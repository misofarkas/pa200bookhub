using BusinessLayer.DTOs;
using BusinessLayer.Services;
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
    public class CustomerServiceTests
    {
        private MockedDependencyInjectionBuilder _serviceProviderBuilder;

        public CustomerServiceTests()
        {
            _serviceProviderBuilder = new MockedDependencyInjectionBuilder()
                .AddServices()
                .AddMockdDBContext();
        }

        [Fact]
        public async Task GetCustomersAsync_WhenCalled_ReturnsAllCustomers()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);

                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

                // Act
                var result = await customerService.GetCustomersAsync();

                // Assert
                Assert.NotNull(result);
                Assert.Equal(TestDataHelper.GetFakeCustomers().Count, result.Count);
            }
        }

        [Fact]
        public async Task DeleteCustomerAsync_ValidId_DeletesCustomer()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);
                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

                var customerIdToDelete = 1;

                // Act
                var result = await customerService.DeleteCustomerAsync(customerIdToDelete);

                // Assert
                Assert.True(result);
                var customer = await dbContext.Customers.FindAsync(customerIdToDelete);
                Assert.True(customer.isDeleted);
            }
        }

        [Fact]
        public async Task GetCustomerAsync_ExistingId_ReturnsCustomer()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);
                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

                var customerToGet = TestDataHelper.GetFakeCustomers().First();

                // Act
                var result = await customerService.GetCustomerAsync(customerToGet.Id);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(customerToGet.Username, result.Username);
            }
        }

        [Fact]
        public async Task CreateCustomerAsync_ValidData_CreatesNewCustomer()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);
                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

                var newCustomerDTO = new CustomerDTO
                {
                    Username = "NewUser",
                };

                // Act
                var result = await customerService.CreateCustomerAsync(newCustomerDTO);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(newCustomerDTO.Username, result.Username);

                var customerInDb = dbContext.Customers.FirstOrDefault(c => c.Username == newCustomerDTO.Username);
                Assert.NotNull(customerInDb);
                Assert.Equal(newCustomerDTO.Username, customerInDb.Username);
            }
        }

        [Fact]
        public async Task UpdateCustomerAsync_ValidId_UpdatesCustomer()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);
                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

                var customerIdToUpdate = TestDataHelper.GetFakeCustomers().First().Id;
                var updatedCustomerDTO = new CustomerDTO { Username = "UpdatedUsername" };

                // Act
                var result = await customerService.UpdateCustomerAsync(customerIdToUpdate, updatedCustomerDTO);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("UpdatedUsername", result.Username);
            }
        }

        [Fact]
        public async Task GetCustomerAsync_DeletedCustomer_ReturnsNull()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);
                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

                var deletedCustomerId = 1; // Assuming the first customer is marked as deleted
                dbContext.Customers.Find(deletedCustomerId).isDeleted = true;
                await dbContext.SaveChangesAsync();

                // Act
                var result = await customerService.GetCustomerAsync(deletedCustomerId);

                // Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task DeleteCustomerAsync_NonExistentCustomer_ReturnsFalse()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);
                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

                var nonExistentCustomerId = 999; // A customer ID that doesn't exist

                // Act
                var result = await customerService.DeleteCustomerAsync(nonExistentCustomerId);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public async Task UpdateCustomerAsync_DeletedCustomer_ReturnsNull()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);
                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

                var deletedCustomerId = 1; // Assuming the first customer is marked as deleted
                dbContext.Customers.Find(deletedCustomerId).isDeleted = true;
                await dbContext.SaveChangesAsync();

                var updatedCustomerDTO = new CustomerDTO { Username = "UpdatedUsername" };

                // Act
                var result = await customerService.UpdateCustomerAsync(deletedCustomerId, updatedCustomerDTO);

                // Assert
                Assert.Null(result);
            }
        }

        [Fact]
        public async Task GetCustomersAsync_EmptyDatabase_ReturnsEmptyList()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                dbContext.Customers.RemoveRange(dbContext.Customers); // Clear the Customers table
                await dbContext.SaveChangesAsync();

                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

                // Act
                var result = await customerService.GetCustomersAsync();

                // Assert
                Assert.NotNull(result);
                Assert.Empty(result);
            }
        }

        [Fact]
        public async Task DeleteCustomerAsync_AlreadyDeletedCustomer_ReturnsFalse()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);

                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

                var alreadyDeletedCustomerId = 1; // Assuming this customer is already marked as deleted
                dbContext.Customers.Find(alreadyDeletedCustomerId).isDeleted = true;
                await dbContext.SaveChangesAsync();

                // Act
                var result = await customerService.DeleteCustomerAsync(alreadyDeletedCustomerId);

                // Assert
                Assert.False(result);
            }
        }

        [Fact]
        public async Task GetCustomerAsync_NonExistentCustomer_ReturnsNull()
        {
            // Arrange
            var serviceProvider = _serviceProviderBuilder.Create();
            using (var scope = serviceProvider.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<BookHubDBContext>();
                await MockedDBContext.PrepareDataAsync(dbContext);

                var customerService = scope.ServiceProvider.GetRequiredService<ICustomerService>();

                var nonExistentCustomerId = 999; // A customer ID that doesn't exist

                // Act
                var result = await customerService.GetCustomerAsync(nonExistentCustomerId);

                // Assert
                Assert.Null(result);
            }
        }
    }
}
