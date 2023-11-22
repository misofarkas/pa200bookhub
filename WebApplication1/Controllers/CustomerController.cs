using BusinessLayer.DTOs;
using BusinessLayer.Services;
using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
	[ApiController]
	[Route("api/Customers")]
	public class CustomerController : ControllerBase
	{
        private readonly ICustomerService _customerService;

        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerList()
        {
            var customers = await _customerService.GetCustomersAsync();
            if (customers == null || customers.Count == 0)
            {
                return NotFound("No customers were found.");
            }
            return Ok(customers);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCustomer(int id)
        {
            var customer = await _customerService.GetCustomerAsync(id);

            if (customer == null)
            {
                return NotFound($"No customer with ID {id} was found.");
            }

            return Ok(customer);
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCustomer(int id)
        {
            var result = await _customerService.DeleteCustomerAsync(id);
            if (!result)
            {
                return NotFound($"No customer with ID {id} was found.");
            }

            return NoContent();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCustomer(CustomerDTO model)
        {
            var result = await _customerService.CreateCustomerAsync(model);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
        
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateCustomer(int id, CustomerDTO model)
        {
            var result = await _customerService.UpdateCustomerAsync(id, model);

            if (result == null)
            {
                return BadRequest();
            }

            return Ok(result);
        }


    }
}
