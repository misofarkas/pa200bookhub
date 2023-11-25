using BusinessLayer.DTOs;
using BusinessLayer.Mapper;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly BookHubDBContext _dbContext;

        public CustomerService(BookHubDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        private IQueryable<Customer> GetAllCustomersQuery()
        {
            return _dbContext.Customers.Where(a => a.isDeleted == false);
        }

        private async Task<List<CustomerDTO>> GetCustomerCommonQuery(IQueryable<Customer> query)
        {
            var customers = await query
                .Include(b => b.Reviews)
                .Include(b => b.Wishlists)
                .Include(b => b.PurchaseHistories)
                .ToListAsync();

            return customers.Select(b => b.MapToCustomerDTO()).ToList();
        }

        public async Task<List<CustomerDTO>> GetCustomersAsync()
        {
            return await GetCustomerCommonQuery(GetAllCustomersQuery());
        }

        
        
        public async Task<bool> DeleteCustomerAsync(int id)
        {
            
            var customer = await _dbContext.Customers.FindAsync(id);

            if (customer == null || customer.isDeleted)
            {
                return false;
            }

            customer.isDeleted = true;
            await _dbContext.SaveChangesAsync();
            return true;
            
        }


        public async Task<CustomerDTO?> GetCustomerAsync(int id)
        {
            var customer = await GetCustomerCommonQuery(GetAllCustomersQuery().Where(a => a.Id == id));
            if (customer == null || !customer.Any())
            {
                return null;
            }

            return customer.FirstOrDefault();
        }

        
        public async Task<CustomerDTO> CreateCustomerAsync(CustomerDTO customerDTO)
        {

            var customer = customerDTO.MapToCustomer();
            _dbContext.Customers.Add(customer);
            await _dbContext.SaveChangesAsync();
            return customer.MapToCustomerDTO();

        }

        public async Task<CustomerDTO?> UpdateCustomerAsync(int id, CustomerDTO customerDTO)
        {
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null || customer.isDeleted)
            {
                return null;
            }
            customer.Username = customerDTO.Username;

            await _dbContext.SaveChangesAsync();
            return customer.MapToCustomerDTO();

        }
    }
}
