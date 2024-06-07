using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankAPI.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class CustomerController : ControllerBase
	{
		private readonly DataAccessService _dataAccessService;
		public CustomerController(DataAccessService dataAccessService)
		{
			_dataAccessService = dataAccessService;
		}

		[HttpGet("me/{id}")]
		public async Task<ActionResult<Customer>> GetCustomerInfo(int id)
		{
			var context = _dataAccessService.GetDbContext();
			var customer = await context.Customers.FindAsync(id);
			if (customer == null)
			{
				return NotFound();
			}
			return customer;
		}

	}
}
