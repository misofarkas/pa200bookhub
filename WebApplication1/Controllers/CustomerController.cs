using DataAccessLayer.Data;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CustomerController : ControllerBase
	{
		private readonly BookHubDBContext _dbContext;

		public CustomerController(BookHubDBContext dbContext)
		{
			_dbContext = dbContext;
		}

		[HttpGet]
		[Route("list")]
		public async Task<IActionResult> GetCustomerList()
		{
			var customers = await _dbContext.Customers
				.Select(c => new CustomerModel
				{
					Id = c.Id,
					Username = c.Username
				})
				.ToListAsync();

			if (customers == null || !customers.Any())
			{
				return BadRequest("No customers were found.");
			}

			return Ok(customers);
		}

		[HttpGet]
		[Route("{id}")]
		public async Task<IActionResult> GetCustomer(int id)
		{
			var customer = await _dbContext.Customers
				.Where(c => c.Id == id)
				.Select(c => new CustomerModel
				{
					Id = c.Id,
					Username = c.Username
				})
				.FirstOrDefaultAsync();

			if (customer == null)
			{
				return BadRequest($"No customer with ID {id} was found.");
			}

			return Ok(customer);
		}

		[HttpPost]
		[Route("create")]
		public async Task<IActionResult> CreateCustomer(CustomerModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var customer = new Customer
			{
				Username = model.Username,
				Password = model.Password
			};

			_dbContext.Customers.Add(customer);
			await _dbContext.SaveChangesAsync();

			return Ok(customer);
		}

		[HttpPut]
		[Route("{id}/update")]
		public async Task<IActionResult> UpdateCustomer(int id, CustomerModel model)
		{
			if (!ModelState.IsValid)
			{
				return BadRequest(ModelState);
			}

			var customer = await _dbContext.Customers.FindAsync(id);

			if (customer == null)
			{
				return BadRequest($"No customer with ID {id} was found.");
			}

			customer.Username = model.Username;

			customer.Password = model.Password;

			_dbContext.Customers.Update(customer);
			await _dbContext.SaveChangesAsync();

			return Ok(customer);
		}

		[HttpDelete]
		[Route("{id}/delete")]
		public async Task<IActionResult> DeleteCustomer(int id)
		{
			var customer = await _dbContext.Customers.FindAsync(id);

			if (customer == null)
			{
				return BadRequest($"No customer with ID {id} was found.");
			}

			_dbContext.Customers.Remove(customer);
			await _dbContext.SaveChangesAsync();

			return Ok();
		}
	}
}
