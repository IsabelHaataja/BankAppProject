using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer.Models;
using DataAccessLayer.Models.CountrySystem;

namespace Services
{
    public interface ITransactionService
    {
        Task<List<Transaction>> GetSuspiciousTransactionsForCustomer(int customerId, DateTime lastProcessedDate, bool isFirstRun);
        Task<List<Customer>> GetCustomersByCountryAsync(Country country);
        Task<bool> IsFirstRunAsync();
    }
}
