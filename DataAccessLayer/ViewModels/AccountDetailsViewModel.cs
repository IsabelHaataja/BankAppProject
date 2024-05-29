using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.ViewModels
{
	public class AccountDetailsViewModel
	{
		public int AccountId { get; set; }
		public string AccountNumber { get; set; }
		public decimal Balance { get; set; }
		public List<TransactionViewModel> Transactions { get; set; }
	}
}
