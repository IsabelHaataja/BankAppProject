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
using DataAccessLayer.Models.CountrySystem;

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
        //public async Task<List<Customer>> GetUsersByCountryAsync(Country country)
        //{
        //    using (var dbContext = _dataAccessService.GetDbContext())
        //    {
        //        return await dbContext.Customers
        //        .Where(c => c.Country == country)
        //        .Include(c => c.Dispositions)
        //        .ThenInclude(d => d.Account)
        //        .ToListAsync();
        //    }
        //}
    }
}
