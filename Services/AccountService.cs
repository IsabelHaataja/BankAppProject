using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class AccountService : IAccountService
	{
		private readonly DataAccessService _dataAccessService;
		public AccountService(DataAccessService dataAccessService)
		{
			_dataAccessService = dataAccessService;
		}
		public ErrorCode Withdraw(int accountId, decimal amount)
		{
			var context = _dataAccessService.GetDbContext();
			var accountDb = context.Accounts.First(a => a.AccountId == accountId);

			if (accountDb.Balance < amount)
			{
				return ErrorCode.BalanceTooLow;
			}

			if (amount < 100 || amount > 10000)
			{
				return ErrorCode.IncorrectAmount;
			}


			// Här skulle man tex. skapa en ny databas entitet som heter "Transaction"
			// ... och fyller den med info här...
			// tex. Date, Amount, Current Balance, Account number etc.
			accountDb.Balance -= amount;
			context.SaveChanges();
			return ErrorCode.OK;
		}

		public ErrorCode Deposit(int accountId, decimal amount, string comment)
		{
			var context = _dataAccessService.GetDbContext();
			var accountDb = context.Accounts.First(a => a.AccountId == accountId);

			if (amount < 100 || amount > 10000)
			{
				return ErrorCode.IncorrectAmount;
			}

			if (String.IsNullOrEmpty(comment))
			{
				return ErrorCode.CommentEmpty;
			}

			// Här skulle man tex. skapa en ny databas entitet som heter "Transaction"
			// ... och fyller den med info här...
			// tex. Date, Amount, Current Balance, Account number etc.

			accountDb.Balance += amount;
			context.SaveChanges();
			return ErrorCode.OK;

		}
		public Account GetAccount(int accountId)
		{
			var context = _dataAccessService.GetDbContext();
			return context.Accounts.First(a => a.AccountId == accountId);
		}
	}
}
