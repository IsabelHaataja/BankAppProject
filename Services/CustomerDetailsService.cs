using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class CustomerDetailsService : ICustomerDetails
	{
		private readonly DataAccessService _context;
		private readonly IMapper _mapper;
		public CustomerDetailsService(DataAccessService context, IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}
		public async Task<CustomerViewModel> GetCustomerAsync(int customerId)
		{
			var chosenCustomer = await _context.GetDbContext().Customers.FindAsync(customerId);

			if (chosenCustomer == null)
			{
				return null;
			}

			var viewModel = _mapper.Map<CustomerViewModel>(chosenCustomer);

			viewModel.Accounts = await _context.GetDbContext().Dispositions
				.Where(d => d.CustomerId == customerId)
				.Select(d => _mapper.Map<AccountViewModel>(d.Account))
				.ToListAsync();

			viewModel.Cards = _context.GetDbContext().Dispositions
				.Where(d => d.CustomerId == customerId)
				.SelectMany(d => d.Cards)
				.Select(c => _mapper.Map<CardViewModel>(c))
				.ToList();
			//viewModel.Accounts = await _context.Dispositions
			//    .Include(x => x.Account)
			//    .Where(d => d.CustomerId == customerId)
			//    .Select(y => new AccountViewModel
			//    {
			//        AccountId = y.Account.AccountId,
			//        Balance = y.Account.Balance,

			//    }).ToListAsync();
			return viewModel;
		}
        public string FormatCardNumber(string cardNumber)
        {
            return string.Join(" ", Enumerable.Range(0, cardNumber.Length / 4)
                .Select(i => cardNumber.Substring(i * 4, 4)));
        }
    }
}

