using DataAccessLayer;
using DataAccessLayer.Models;
using DataAccessLayer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly DataAccessService _dataAccessService;
        private readonly IMapper _mapper;
        public CustomerService(DataAccessService dbContext, IMapper mapper)
        {
            _dataAccessService = dbContext;
            _mapper = mapper;
        }


        public IEnumerable<Customer> GetCustomers()
        {
            using (var dbContext = _dataAccessService.GetDbContext())
            {
                return dbContext.Customers;
            }
        }
        public Customer GetCustomer(int customerId)
        {
            using (var dbContext = _dataAccessService.GetDbContext())
            {
                return dbContext.Customers.FirstOrDefault(c => c.CustomerId == customerId);
            }
        }

        public int SaveNew(Customer customer)
        {
            using (var dbContext = _dataAccessService.GetDbContext())
            {
                dbContext.Customers.Add(customer);
                dbContext.SaveChanges();
                return customer.CustomerId;
            }
        }

        public void Update()
        {
            using (var dbContext = _dataAccessService.GetDbContext())
            {
                dbContext.SaveChanges();
            }
        }

        public PagedResult<CustomerSearchViewModel> ReadCustomers(string sortColumn, string sortOrder, int page, string searchText = null)
        {
            var query = _dataAccessService.GetCustomersQuery();

            if (!string.IsNullOrEmpty(searchText))
            {
                query = query.Where(c =>
                    c.CustomerId.ToString().Contains(searchText) ||
                    c.Givenname.Contains(searchText) ||
                    c.City.Contains(searchText)
                );

            }

            switch (sortColumn)
            {
                case "Givenname":
                    query = sortOrder == "asc" ? query.OrderBy(c => c.Givenname) : query.OrderByDescending(c => c.Givenname);
                    break;
                case "City":
                    query = sortOrder == "asc" ? query.OrderBy(c => c.City) : query.OrderByDescending(c => c.City);
                    break;
                default:
                    query = query.OrderBy(c => c.CustomerId);
                    break;
            }

            var pagedCustomers = query.Select(c => _mapper.Map<CustomerSearchViewModel>(c))
                                      .GetPaged(page, 50);
            return pagedCustomers;
        }

		public void AssignUniqueAccountNumbers()
		{
			var duplicateAccountNumbers = _dataAccessService.GetDuplicateAccountNumbers();

			var duplicateAccounts = _dataAccessService.GetDuplicateAccounts(duplicateAccountNumbers);

			foreach (var group in duplicateAccounts.GroupBy(a => a.AccountNumber))
			{
				foreach (var account in group.Skip(1))
				{
					account.AccountNumber = GenerateAccountNumber();
				}
			}
            _dataAccessService.GetDbContext().SaveChanges();
		}

        public void AssignAccountNumbers()
        {

            var dbContext = _dataAccessService.GetDbContext();
            var accounts = dbContext.Accounts.Where(a => a.AccountNumber == null).ToList();

            foreach (var account in accounts)
            {
                account.AccountNumber = GenerateAccountNumber();
            }

            AssignUniqueAccountNumbers();

            dbContext.SaveChanges();
        }

        private string GenerateAccountNumber()
		{
			return "ACCT-" + Guid.NewGuid().ToString().Substring(0, 6).ToUpper();
		}
	}
}
