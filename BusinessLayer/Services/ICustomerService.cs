using BusinessLayer.DTOs;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Services
{
    public interface ICustomerService : IBaseService
    {

        Task<List<CustomerDTO>> GetCustomersAsync();

        Task<CustomerDTO?> GetCustomerAsync(int id);

        Task<CustomerDTO> CreateCustomerAsync(CustomerDTO customer);

        Task<CustomerDTO?> UpdateCustomerAsync(int id, CustomerDTO customer);

        Task<bool> DeleteCustomerAsync(int id);
    }
}
