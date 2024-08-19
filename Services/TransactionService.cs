using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TransactionService
    {
        private readonly BankAppDataContext _bankAppDataContext;

        public TransactionService(BankAppDataContext bankAppDataContext)
        {
            _bankAppDataContext = bankAppDataContext;
        }

        public async Task<List<Transaction>> GetSuspiciousTransactionsForCustomer(int customerId, DateTime lastProcessedDate)
        {
            var now = DateTime.UtcNow;
            var threeDaysAgo = DateOnly.FromDateTime(now.AddDays(-3));

            // Get all transactions since last processed date
            var recentTransactions = await _bankAppDataContext.Transactions
                .Include(t => t.AccountNavigation)
                .ThenInclude(a => a.Dispositions)
                .Where(t => t.AccountNavigation.Dispositions.Any(d => d.CustomerId == customerId)
                            && t.Date.ToDateTime(TimeOnly.MinValue) > lastProcessedDate)
                .ToListAsync();

            //Single transaction over 15,000 kr
            var largeTransactions = recentTransactions
                .Where(t => t.Amount > 15000)
                .ToList();

            //Total transactions in the last three days over 23,000 kr
            var recentTransactionsSum = recentTransactions
                .Where(t => t.Date >= threeDaysAgo)
                .GroupBy(t => t.AccountId)
                .Select(g => new { AccountId = g.Key, Total = g.Sum(t => t.Amount) })
                .Where(g => g.Total > 23000)
                .SelectMany(g => recentTransactions.Where(t => t.AccountId == g.AccountId))
                .ToList();

            largeTransactions.AddRange(recentTransactionsSum);
            return largeTransactions.Distinct().ToList();
        }
    }
}
