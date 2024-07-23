using AutoMapper;
using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.ViewModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Client;
using Services.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
	public class AccountService /*: IAccountService*/
	{
		//private readonly DataAccessService _dataAccessService;
		//private readonly IMapper _mapper;
		//public AccountService(DataAccessService dataAccessService, IMapper mapper)
		//{
		//	_dataAccessService = dataAccessService;
		//	_mapper = mapper;
		//}
		//public ErrorCode Withdraw(int accountId, decimal amount)
		//{
		//	var context = _dataAccessService.GetDbContext();
		//	var accountDb = context.Accounts.First(a => a.AccountId == accountId);

		//	if (accountDb.Balance < amount)
		//	{
		//		return ErrorCode.BalanceTooLow;
		//	}

		//	if (amount < 100 || amount > 10000)
		//	{
		//		return ErrorCode.IncorrectAmount;
		//	}

		//	var transaction = new Transaction
		//	{
		//		AccountId = accountId,
		//		Date = DateOnly.FromDateTime(DateTime.UtcNow),
		//		Type = "Debit",
		//		Operation = "Withdrawal",
		//		Amount = -amount,
		//		Balance = accountDb.Balance - amount,
		//	};
			
		//	accountDb.Balance -= amount;
		//	context.Transactions.Add(transaction);
		//	context.SaveChanges();
		//	return ErrorCode.OK;
		//}

		//public ErrorCode Deposit(int accountId, decimal amount, string comment)
		//{
		//	var context = _dataAccessService.GetDbContext();
		//	var accountDb = context.Accounts.FirstOrDefault(a => a.AccountId == accountId);

  //          Console.WriteLine($"Amount received for validation: {accountId}, {amount}, {comment}");

		//	if (amount < 100 || amount > 10000)
		//	{
		//		return ErrorCode.IncorrectAmount;
		//	}

		//	if (String.IsNullOrEmpty(comment))
		//	{
		//		return ErrorCode.CommentEmpty;
		//	}

		//	var transaction = new Transaction
		//	{
		//		AccountId = accountId,
		//		Date = DateOnly.FromDateTime(DateTime.UtcNow),
		//		Type = "Credit",
		//		Operation = "Deposit",
		//		Amount = amount,
		//		Balance = accountDb.Balance + amount,
		//		Symbol = comment
		//	};

		//	accountDb.Balance += amount;
		//	context.Transactions.Add(transaction);
		//	context.SaveChanges();
		//	return ErrorCode.OK;

		//}

		//public ErrorCode Transfer(int fromAccountId, string toAccountNumber, decimal amount, string comment)
		//{
  //          var context = _dataAccessService.GetDbContext();
  //          var fromAccount = context.Accounts.FirstOrDefault(a => a.AccountId == fromAccountId);
  //          var toAccount = context.Accounts.FirstOrDefault(a => a.AccountNumber == toAccountNumber);

  //          if (fromAccount == null || toAccount == null)
  //              return ErrorCode.AccountNotFound;

  //          if (fromAccount.Balance < amount)
  //              return ErrorCode.BalanceTooLow;

  //          var transaction = new Transaction
  //          {
  //              AccountId = fromAccountId,
  //              Date = DateOnly.FromDateTime(DateTime.UtcNow),
  //              Type = "Debit",
  //              Operation = "Transfer Out",
  //              Amount = -amount,
  //              Balance = fromAccount.Balance - amount,
  //              Symbol = comment
  //          };
			
		//	fromAccount.Balance -= amount;
  //          context.Transactions.Add(transaction);

  //          var recipientTransaction = new Transaction
  //          {
  //              AccountId = toAccount.AccountId,
  //              Date = DateOnly.FromDateTime(DateTime.UtcNow),
  //              Type = "Credit",
  //              Operation = "Transfer In",
  //              Amount = amount,
  //              Balance = fromAccount.Balance + amount,
  //              Symbol = comment
  //          };

		//	toAccount.Balance += amount;
		//	context.Transactions.Add(recipientTransaction);

  //          context.SaveChanges();
  //          return ErrorCode.OK;
  //      }
		//public AccountDetailsViewModel GetAccountDetails(int accountId, int skip = 0, int take = 20)
		//{
		//	var context = _dataAccessService.GetDbContext();
		//	var accountDb = context.Accounts.Include(a => a.Transactions).First(a => a.AccountId == accountId);

		//	var viewmodel = _mapper.Map<AccountDetailsViewModel>(accountDb);
		//	viewmodel.Transactions = accountDb.Transactions
		//		.OrderByDescending(t => t.Date)
		//		.Skip(skip)
		//		.Take(take)
		//		.Select(t => _mapper.Map<TransactionViewModel>(t))
		//		.ToList();

		//	return viewmodel;
		//}
		//public List<TransactionViewModel> GetMoreTransactions(int accountId, int skip, int take)
		//{
		//	var context = _dataAccessService.GetDbContext();
		//	return context.Transactions
		//		.Where(t => t.AccountId == accountId)
		//		.OrderByDescending(t => t.Date)
		//		.Skip(skip)
		//		.Take(take)
		//		.Select(t => _mapper.Map<TransactionViewModel>(t))
		//		.ToList();
		//}
		//public Account GetAccount(int accountId)
		//{
		//	var context = _dataAccessService.GetDbContext();
		//	return context.Accounts.First(a => a.AccountId == accountId);
		//}
	}
}
