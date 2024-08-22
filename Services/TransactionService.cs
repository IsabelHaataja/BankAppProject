using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.Models.CountrySystem;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class TransactionService : ITransactionService
    {
        private readonly BankAppDataContext _bankAppDataContext;

        public TransactionService(BankAppDataContext bankAppDataContext)
        {
            _bankAppDataContext = bankAppDataContext;
        }

        public async Task<List<Transaction>> GetSuspiciousTransactionsForCustomer(int customerId, DateTime lastProcessedDate, bool isFirstRun)
        {
            var now = DateTime.UtcNow;
            var startDate = isFirstRun ? now.AddDays(-1) : lastProcessedDate;

            // Get all transactions since last processed date
            var transactions = await _bankAppDataContext.Transactions
                .Include(t => t.AccountNavigation)
                .ThenInclude(a => a.Dispositions)
                .Where(t => t.AccountNavigation.Dispositions.Any(d => d.CustomerId == customerId))
                .ToListAsync();

            // Convert DateOnly to DateTime for comparison
            var startDateTime = startDate;
            var threeDaysAgo = now.AddDays(-3);

            var recentTransactions = transactions
                .Where(t => t.Date.ToDateTime(TimeOnly.MinValue) > startDateTime)
                .ToList();

            // Single transaction over 15,000 kr
            var largeTransactions = recentTransactions
                .Where(t => t.Amount > 15000)
                .ToList();

            // Total transactions in the last three days over 23,000 kr
            var recentTransactionsSum = recentTransactions
                .Where(t => t.Date.ToDateTime(TimeOnly.MinValue) >= threeDaysAgo)
                .GroupBy(t => t.AccountId)
                .Select(g => new { AccountId = g.Key, TotalAmount = g.Sum(t => t.Amount) })
                .Where(g => g.TotalAmount > 23000)
                .SelectMany(g => recentTransactions.Where(t => t.AccountId == g.AccountId))
                .ToList();

            largeTransactions.AddRange(recentTransactionsSum);
            return largeTransactions.Distinct().ToList();
        }
        public async Task<List<Customer>> GetCustomersByCountryAsync(Country country)
        {
                return await _bankAppDataContext.Customers
                .Where(c => c.Country == country)
                .Include(c => c.Dispositions)
                .ThenInclude(d => d.Account)
                .ToListAsync();
        }
        public async Task<bool> IsFirstRunAsync()
        {
            var count = await _bankAppDataContext.LastProcesseds.CountAsync();
            return count == 0;
        }
    }
}
