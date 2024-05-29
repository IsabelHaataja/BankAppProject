using DataAccessLayer;
using Microsoft.AspNetCore.Mvc;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;

namespace BankProjectApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class AccountController : ControllerBase
	{
		private readonly DataAccessService _dataAccessService;
		public AccountController(DataAccessService dataAccessService)
		{
			_dataAccessService = dataAccessService;
		}

		[HttpGet("{id}/{limit}/{offset}")]
		public async Task<ActionResult<IEnumerable<Transaction>>> GetTransactions(int id, int limit, int offset)
		{
			var context = _dataAccessService.GetDbContext();
			var transactions = await context.Transactions
				.Where(t => t.AccountId == id)
				.Skip(offset)
				.Take(limit)
				.ToListAsync();
			
			if (!transactions.Any())
			{
				return NotFound();
			}

			return transactions;
		}
	}
}
