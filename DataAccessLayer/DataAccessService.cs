using DataAccessLayer.Models;
using DataAccessLayer.Models.CountrySystem;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class DataAccessService
    {
  //      private readonly Func<BankAppDataV2Context> _contextFactory;
  //      private readonly BankAppDataV2Context _context;
  //      public DataAccessService (Func<BankAppDataV2Context> contextFactory)
  //      {
  //          _contextFactory = contextFactory;
  //          _context = _contextFactory();
  //      }
  //      public IQueryable<Customer> GetCustomersQuery()
  //      {
  //          return _context.Customers.AsQueryable();
  //      }
  //      public BankAppDataV2Context GetDbContext()
  //      {
  //          return _contextFactory();
  //      }
  //      public List<int> GetCustomerIdByCountry(Country country)
  //      {
  //          try
  //          {
  //              int countryValue= (int)country;
  //              return _context.Dispositions
  //                 .Where(d => (int)d.Customer.CustomerCountry == countryValue)
  //                 .Select(d => d.CustomerId)
  //                 .Distinct()
  //                 .ToList();
  //          }
  //          catch (Exception ex)
  //          {
  //              Console.WriteLine($"Error in GetCustomerIdByCountry: {ex.Message}");
  //              return new List<int>();
  //          }

  //      }
  //      public decimal GetTotalBalanceByCountry(List<int> customerIds)
  //      {
  //          try
  //          {
  //              return _context.Dispositions
  //                  .Where(d => customerIds.Contains(d.CustomerId))
  //                  .Sum(d => d.Account.Balance);
  //          }
  //          catch(Exception ex) 
  //          {
  //              Console.WriteLine($"Error in GetTotalBalanceByCountry: {ex.Message}");
  //              return 0;
  //          }
  //      }
  //      public int GetAccountCountByCountry(Country country)
  //      {
  //          try
  //          {
  //              return _context.Dispositions
  //                  .Where(d => (int)d.Customer.CustomerCountry == (int)country)
  //                  .Select(d => d.AccountId)
  //                  .Distinct()
  //                  .Count();
  //          }
  //          catch (Exception ex)
  //          {
  //              Console.WriteLine($"Error in GetAccountCountByCountry: {ex.Message}");
  //              return 0;
  //          }

  //      }
		//public List<string> GetDuplicateAccountNumbers()
		//{
		//	return _context.Accounts
		//		.GroupBy(a => a.AccountNumber)
		//		.Where(g => g.Count() > 1 && g.Key != null)
		//		.Select(g => g.Key)
		//		.ToList();
		//}
		//public List<Account> GetDuplicateAccounts(List<string> duplicateAccountNumbers)
		//{
		//	return _context.Accounts
		//		.Where(a => duplicateAccountNumbers.Contains(a.AccountNumber))
		//		.OrderBy(a => a.AccountNumber) 
		//		.ToList();
		//}

	}
}
