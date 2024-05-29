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
		public static bool IsSuspicious(Transaction transaction, BankAppDataV2Context context)
		{
			var recentTransactions = context.Transactions
				.Where(t => t.AccountId == transaction.AccountId && t.Date.ToDateTime(new TimeOnly()) >= DateTime.Now.AddHours(-72))
				.Sum(t => t.Amount);

			return recentTransactions > 23000;
		}
	}
}
