using DataAccessLayer.Models;
using DataAccessLayer.ViewModels;
using Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public interface IAccountService
	{
		//Account GetAccount(int accountId);
		ErrorCode Withdraw(int accountId, decimal amount);
		ErrorCode Deposit(int accountId, decimal amount, string comment);
		ErrorCode Transfer(int fromAccountId, string toAccountNumber, decimal amount, string comment);

        AccountDetailsViewModel GetAccountDetails(int accountId, int skip = 0, int take = 20);
		List<TransactionViewModel> GetMoreTransactions(int accountId, int skip, int take);
	}
}
