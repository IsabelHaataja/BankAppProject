using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;

namespace BatchProcessor
{
	public class SuspiciousTransaction
	{
		public int CustomerId { get; set; }
		public int AccountId { get; set; }
		public int TransactionId { get; set; }
		public decimal Amount { get; set; }
		public DateOnly Date { get; set; }
		//public static bool IsSuspicious(Transaction transaction, BankAppDataV2Context context)
		//{
  //          //Check if amount is over 15 000
  //          if (transaction.Amount > 15000)
  //          {
  //              Console.WriteLine($"Transaction {transaction.TransactionId} is suspicious due to amount: {transaction.Amount}");
  //              return true;
  //          }

  //          //check if total transactions during last 3 days is over 23 000
  //          var threeDaysAgo = DateOnly.FromDateTime(DateTime.Now.AddDays(-3));
  //          var recentTransactionsTotal = context.Transactions
  //              .Where(t => t.AccountId == transaction.AccountId && t.Date >= threeDaysAgo && t.TransactionId != transaction.TransactionId)
  //              .Sum(t => t.Amount);

  //          if (recentTransactionsTotal > 23000)
  //          {
  //              Console.WriteLine($"Transaction {transaction.TransactionId} contributes to a suspicious total over the last 3 days: {recentTransactionsTotal}");
  //              return true;
  //          }

  //          return false;
  //      }		
	}
}
